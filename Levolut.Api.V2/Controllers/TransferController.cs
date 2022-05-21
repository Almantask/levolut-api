using Levolut.Api.V2.CommandHandlers;
using Levolut.Api.V2.Contracts;
using Levolut.Api.V2.Exceptions;
using Levolut.Api.V2.Infrastructure.Database;
using Levolut.Api.V2.Infrastructure.Database.Models;
using Levolut.Api.V2.QueryHandlers;
using Levolut.Api.V2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Levolut.Api.V2.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class TransferController : ControllerBase
    {
        private readonly IBalanceService _balanceService;

        public TransferController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [HttpGet("Balance/{bankId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetBalanceResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBalance(long userId, long bankId)
        {
            var balance = _balanceService.GetCurrentBalance(userId, bankId);
            return Ok(new GetBalanceResponse { Balance = balance });
        }

        [HttpPost("Add/{bankId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddMoneyResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddMoney(long bankId, long userId, [FromBody] AddMoneyRequest request)
        {
            var balance = _balanceService.AddMoney(userId, bankId, request.MoneyExchange);
            return Ok(new AddMoneyResponse { Balance = balance });
        }

        [HttpPost("Cash/{bankId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CashMoneyResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CashMoney(long bankId, long userId, [FromBody] CashMoneyRequest request)
        {
            var balance = _balanceService.CashMoney(bankId, userId, request.MoneyExchange);
            return Ok(new CashMoneyResponse { Balance = balance });
        }
    }

    public interface IBalanceService
    {
        // Why have 2 methods when you could simply pass a delegate to specify what to do?
        decimal AddMoney(long userId, long bankId, MoneyExchange moneyExchange);
        decimal CashMoney(long bankId, long userId, MoneyExchange moneyExchange);
        decimal GetCurrentBalance(long bankId, long userId);
    }

    public class BalanceService : IBalanceService
    {
        private readonly IQueryHandler<GetCurrentBalanceQuery, Balance> _getCurrentBalanceQueryHandler;
        private readonly IMoneyExchanger _moneyExchanger;
        private readonly ICommandHandler<AddBalanceCommand, Balance> _addBalanceCommandHandler;

        public BalanceService(IQueryHandler<GetCurrentBalanceQuery, Balance> getCurrentBalanceQueryHandler, IMoneyExchanger moneyExchanger, ICommandHandler<AddBalanceCommand, Balance> addBalanceCommandHandler)
        {
            _getCurrentBalanceQueryHandler = getCurrentBalanceQueryHandler;
            _moneyExchanger = moneyExchanger;
            _addBalanceCommandHandler = addBalanceCommandHandler;
        }

        public decimal GetCurrentBalance(long bankId, long userId)
        {
            // Is it okay to be pragmatic returning a whole balance (but use the same handler)?
            var balance = _getCurrentBalanceQueryHandler.Handle(new GetCurrentBalanceQuery(bankId, userId));
            if (balance == null)
            {
                throw new EntityNotFoundException("Balance");
            }

            return balance.Amount;
        }

        public decimal AddMoney(long userId, long bankId, MoneyExchange request)
            => TransferMoney(Add, bankId, userId, request);

        public decimal CashMoney(long bankId, long userId, MoneyExchange request)
            => TransferMoney(Take, bankId, userId, request);

        private decimal TransferMoney(
            Func<decimal, decimal, decimal> addOrTake,
            long bankId, long userId, MoneyExchange moneyExchange)
        {
            var currentBalance = _getCurrentBalanceQueryHandler.Handle(new GetCurrentBalanceQuery(bankId, userId));
            var exchanged = _moneyExchanger.Exchange(bankId, moneyExchange, currentBalance?.Currency ?? moneyExchange.Currency);

            var newBalance = new Balance
            {
                UserId = userId,
                BankId = bankId,
                Amount = addOrTake(currentBalance?.Amount ?? 0, exchanged),
                CreatedAt = DateTime.UtcNow,
            };

            _addBalanceCommandHandler.Handle(new AddBalanceCommand(newBalance));

            return newBalance.Amount;
        }

        private static decimal Add(decimal currentAmount, decimal exchanged) => currentAmount + exchanged;
        private static decimal Take(decimal currentAmount, decimal exchanged) => currentAmount - exchanged;
    }

    public interface IMoneyExchanger
    {
        decimal Exchange(long bankId, MoneyExchange moneyExchange, Currency fromCurrency);
    }

    public class MoneyExchanger : IMoneyExchanger
    {
        private readonly ICurrencyProvider _currencyProvider;
        private IQueryHandler<GetBankFeeRuleQuery, BankFeeRule> _getBankFeeRuleQueryHandler;

        public MoneyExchanger(ICurrencyProvider currencyProvider, IQueryHandler<GetBankFeeRuleQuery, BankFeeRule> getBankFeeRuleQueryHandler)
        {
            _currencyProvider = currencyProvider;
            _getBankFeeRuleQueryHandler = getBankFeeRuleQueryHandler;
        }

        public decimal Exchange(long bankId, MoneyExchange moneyExchange, Currency fromCurrency)
        {
            var bankFeeRule = _getBankFeeRuleQueryHandler.Handle(new GetBankFeeRuleQuery(bankId));
            RequireNotBlockedCountry(bankFeeRule.BlockedCountries, moneyExchange.FromCountry);
            var exchanged = ExchangeMoney(moneyExchange, fromCurrency, bankFeeRule.Fee);

            return exchanged;
        }

        private decimal ExchangeMoney(MoneyExchange moneyExchange, Currency fromCurrency, decimal fee)
        {
            var exchangeFee = moneyExchange.Currency == fromCurrency ? 0 : fee;
            var requestCurrencyRate = _currencyProvider.GetRate(moneyExchange.Currency);
            var currentBalanceCurrencyRate = _currencyProvider.GetRate(fromCurrency);
            var exchanged = moneyExchange.Amount / currentBalanceCurrencyRate * requestCurrencyRate * (1 - exchangeFee);

            return exchanged;
        }

        private void RequireNotBlockedCountry(IEnumerable<BlockedCountry> blockedCountries, string country)
        {
            if (blockedCountries.Any(bc => bc.Name == country))
            {
                throw new InvalidOperationException("Country is blocked.");
            }
        }
    }

    public class GetBankFeeRuleQueryHandler : IQueryHandler<GetBankFeeRuleQuery, BankFeeRule>
    {
        private readonly LevolutDbContext context;

        public GetBankFeeRuleQueryHandler(LevolutDbContext context)
        {
            this.context = context;
        }

        public BankFeeRule Handle(GetBankFeeRuleQuery query)
        {
            BankFeeRule bankFeeRule;
            try
            {
                bankFeeRule = context.BankFeeRules
                    .Include(s => s.BlockedCountries)
                    .First(banckFeeRule => banckFeeRule.BankId == query.BankId);
                return bankFeeRule;
            }
            catch (InvalidOperationException)
            {
                throw new EntityNotFoundException("Bank fee rule");
            }
        }
    }

    public record GetBankFeeRuleQuery(long BankId);

    public class AddBalanceCommandHandler : ICommandHandler<AddBalanceCommand, Balance>
    {
        private readonly LevolutDbContext _context;

        public AddBalanceCommandHandler(LevolutDbContext context)
        {
            _context = context;
        }

        public Balance Handle(AddBalanceCommand command)
        {
            var balanceEntityEntry = _context.Balances.Add(command.Balance);
            _context.SaveChanges();

            return balanceEntityEntry.Entity;
        }
    }

    public record AddBalanceCommand(Balance Balance);

    public class GetCurrentBalanceQueryHandler : IQueryHandler<GetCurrentBalanceQuery, Balance>
    {
        private readonly LevolutDbContext _context;

        public GetCurrentBalanceQueryHandler(LevolutDbContext context)
        {
            _context = context;
        }

        public Balance Handle(GetCurrentBalanceQuery query)
        {
            // Why didn't we throw an exception when balance wasn't found here?
            var balance = _context.Balances
                    .Where(b => b.UserId == query.UserId && b.BankId == query.BankId)
                    .OrderByDescending(b => b.CreatedAt)
                    .FirstOrDefault();

            return balance;
        }
    }

    public record GetCurrentBalanceQuery(long BankId, long UserId);

    public record MoneyExchange(decimal Amount, Currency Currency, string FromCountry);
}
using Levolut.Api.V2.Contracts;
using Levolut.Api.V2.Infrastructure.Database;
using Levolut.Api.V2.Infrastructure.Database.Models;
using Levolut.Api.V2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Levolut.Api.V2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController : ControllerBase
    {

        private readonly ILogger<TransferController> _logger;
        private readonly LevolutDbContext context;
        private readonly ICurrencyProvider currencyProvider;

        public TransferController(ILogger<TransferController> logger, LevolutDbContext context, ICurrencyProvider currencyProvider)
        {
            _logger = logger;
            this.context = context;
            this.currencyProvider = currencyProvider;
        }

        [HttpGet("Balance/{bankId}/{userId}")]
        public IActionResult GetBalance(long userId, long bankId)
        {
            try
            {
                var balance = context.Balances
                    .Where(b => b.UserId == userId && b.BankId == bankId)
                    .OrderByDescending(b => b.CreatedAt)
                    .Select(b => b.Amount)
                    .First();

                return Ok(new GetBalanceResponse
                {
                    Balance = balance,
                });
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPost("Add/{bankId}/{userId}")]
        public IActionResult AddMoney(long bankId, long userId, [FromBody] AddMoneyRequest request)
        {
            var balance = context.Balances
                     .Where(b => b.UserId == userId && b.BankId == bankId)
                     .OrderByDescending(b => b.CreatedAt)
                     .FirstOrDefault();

            BankFeeRule bankFeeRule;
            try
            {
                bankFeeRule = context.BankFeeRules
                    .Include(s => s.BlockedCountries)
                    .First(banckFeeRule => banckFeeRule.BankId == bankId);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Bank Fee rule not found.");
            }

            var exchangeFee = request.Currency == (balance?.Currency ?? request.Currency) ? 0 : bankFeeRule.Fee;
            var blockedCountries = bankFeeRule.BlockedCountries;

            var requestCurrencyRate = currencyProvider.GetRate(request.Currency);
            var currentBalanceCurrencyRate = currencyProvider.GetRate(balance?.Currency ?? request.Currency);

            if (blockedCountries.Any(bc => bc.Name == request.Country))
            {
                throw new InvalidOperationException("Country is blocked.");
            }

            Balance balanceEntity = new Balance
            {
                UserId = userId,
                BankId = bankId,
                Amount = (balance?.Amount ?? 0) + request.Amount / currentBalanceCurrencyRate * requestCurrencyRate * (1 - exchangeFee),
                CreatedAt = DateTime.UtcNow,
            };

            context.Balances.Add(balanceEntity);

            context.SaveChanges();


            return Ok(new AddMoneyResponse
            {
                Balance = balanceEntity.Amount
            });
        }

        [HttpPost("Cash/{bankId}/{userId}")]
        public IActionResult CashMoney(long bankId, long userId, [FromBody] CashMoneyRequest request)
        {
            var balance = context.Balances
                      .Where(b => b.UserId == userId && b.BankId == bankId)
                      .OrderByDescending(b => b.CreatedAt)
                      .First();

            var bankFeeRule = context.BankFeeRules
                    .Include(s => s.BlockedCountries)
                .First(banckFeeRule => banckFeeRule.BankId == bankId);

            var exchangeFee = request.Currency == balance.Currency ? 0 : bankFeeRule.Fee;
            var blockedCountries = bankFeeRule.BlockedCountries;

            var requestCurrencyRate = currencyProvider.GetRate(request.Currency);
            var currentBalanceCurrencyRate = currencyProvider.GetRate(balance.Currency);

            if (blockedCountries.Any(bc => bc.Name == request.Country))
            {
                throw new InvalidOperationException("Country is blocked.");
            }

            var currentBalanceOfRequestCurrency = balance.Amount * (1 - exchangeFee) / requestCurrencyRate * currentBalanceCurrencyRate;

            if (currentBalanceOfRequestCurrency < request.Amount)
            {
                throw new InvalidOperationException("Not enough money.");
            }

            context.Balances.Add(new Balance
            {
                UserId = userId,
                BankId = bankId,
                Amount = balance.Amount - request.Amount / currentBalanceCurrencyRate * requestCurrencyRate,
                CreatedAt = DateTime.UtcNow,
            });

            context.SaveChanges();

            return Ok(new CashMoneyResponse
            {
                Balance = balance.Amount
            });
        }
    }
}
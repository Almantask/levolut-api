using Levolut.Api.V2.Database.Command.Commands;
using Levolut.Api.V2.Database.Command.Handlers;
using Levolut.Api.V2.Database.Models;
using Levolut.Api.V2.Database.Query.Handlers;
using Levolut.Api.V2.Database.Query.Queries;
using Levolut.Api.V2.Exceptions;

namespace Levolut.Api.V2.Services;

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
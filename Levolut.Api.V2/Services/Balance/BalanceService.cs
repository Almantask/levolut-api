using Levolut.Api.V2.Database.Command.Commands;
using Levolut.Api.V2.Database.Command.Handlers;
using Levolut.Api.V2.Database.Query.Handlers;
using Levolut.Api.V2.Database.Query.Queries;
using Levolut.Api.V2.Exceptions;
using Levolut.Api.V2.Services.MoneyExchange;

namespace Levolut.Api.V2.Services.Balance;

public class BalanceService : IBalanceService
{
    private readonly IQueryHandler<GetCurrentBalanceQuery, Database.Entities.Balance> _getCurrentBalanceQueryHandler;
    private readonly IMoneyExchanger _moneyExchanger;
    private readonly ICommandHandler<AddBalanceCommand, Database.Entities.Balance> _addBalanceCommandHandler;

    public BalanceService(IQueryHandler<GetCurrentBalanceQuery, Database.Entities.Balance> getCurrentBalanceQueryHandler, IMoneyExchanger moneyExchanger, ICommandHandler<AddBalanceCommand, Database.Entities.Balance> addBalanceCommandHandler)
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

    public decimal AddMoney(long userId, long bankId, Models.MoneyExchange request)
        => TransferMoney(Add, bankId, userId, request);

    public decimal CashMoney(long bankId, long userId, Models.MoneyExchange request)
        => TransferMoney(Take, bankId, userId, request);

    private decimal TransferMoney(
        Func<decimal, decimal, decimal> addOrTake,
        long bankId, long userId, Models.MoneyExchange moneyExchange)
    {
        var currentBalance = _getCurrentBalanceQueryHandler.Handle(new GetCurrentBalanceQuery(bankId, userId));
        var exchanged = _moneyExchanger.Exchange(bankId, moneyExchange, currentBalance?.Currency ?? moneyExchange.Currency);

        var newBalance = new Database.Entities.Balance
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
    private static decimal Take(decimal currentAmount, decimal exchanged)
    {
        if (currentAmount < exchanged)
        {
            throw new InvalidOperationException("You don't have enough money to cash.");
        }
        return currentAmount - exchanged;
    }
}
using Levolut.Api.V3.Domain.DomainServices.Command;
using Levolut.Api.V3.Domain.DomainServices.Query;
using Levolut.Api.V3.Domain.Exceptions;
using Levolut.Api.V3.Domain.Services.MoneyExchange;

namespace Levolut.Api.V3.Domain.Services.Balance;

public class BalanceService : IBalanceService
{
    private readonly IQueryHandler<GetCurrentBalanceQuery, Models.Entities.Balance> _getCurrentBalanceQueryHandler;
    private readonly IMoneyExchanger _moneyExchanger;
    private readonly ICommandHandler<AddBalanceCommand, Models.Entities.Balance> _addBalanceCommandHandler;

    public BalanceService(IQueryHandler<GetCurrentBalanceQuery, Models.Entities.Balance> getCurrentBalanceQueryHandler, IMoneyExchanger moneyExchanger, ICommandHandler<AddBalanceCommand, Models.Entities.Balance> addBalanceCommandHandler)
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

    public decimal AddMoney(long userId, long bankId, Models.ValueObjects.MoneyExchange request)
        => TransferMoney(Add, bankId, userId, request);

    public decimal CashMoney(long bankId, long userId, Models.ValueObjects.MoneyExchange request)
        => TransferMoney(Take, bankId, userId, request);

    private decimal TransferMoney(
        Func<decimal, decimal, decimal> addOrTake,
        long bankId, long userId, Models.ValueObjects.MoneyExchange moneyExchange)
    {
        var currentBalance = _getCurrentBalanceQueryHandler.Handle(new GetCurrentBalanceQuery(bankId, userId));
        var exchanged = _moneyExchanger.Exchange(bankId, moneyExchange, currentBalance?.Currency ?? moneyExchange.Currency);

        var newBalance = new Domain.Models.Entities.Balance
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
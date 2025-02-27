namespace Levolut.Api.V3.Domain.Services.Balance;

public interface IBalanceService
{
    // Why have 2 methods when you could simply pass a delegate to specify what to do?
    decimal AddMoney(long userId, long bankId, Models.ValueObjects.MoneyExchange moneyExchange);
    decimal CashMoney(long bankId, long userId, Models.ValueObjects.MoneyExchange moneyExchange);
    decimal GetCurrentBalance(long bankId, long userId);
}
namespace Levolut.Api.V2.Services;

public interface IBalanceService
{
    // Why have 2 methods when you could simply pass a delegate to specify what to do?
    decimal AddMoney(long userId, long bankId, MoneyExchange moneyExchange);
    decimal CashMoney(long bankId, long userId, MoneyExchange moneyExchange);
    decimal GetCurrentBalance(long bankId, long userId);
}
namespace Levolut.Api.V2.Services.Balance;

public interface IBalanceService
{
    // Why have 2 methods when you could simply pass a delegate to specify what to do?
    decimal AddMoney(long userId, long bankId, Models.MoneyExchange moneyExchange);
    decimal CashMoney(long bankId, long userId, Models.MoneyExchange moneyExchange);
    decimal GetCurrentBalance(long bankId, long userId);
}
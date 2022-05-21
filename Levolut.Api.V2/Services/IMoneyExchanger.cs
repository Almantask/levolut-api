using Levolut.Api.V2.Database.Models;

namespace Levolut.Api.V2.Services;

public interface IMoneyExchanger
{
    decimal Exchange(long bankId, MoneyExchange moneyExchange, Currency fromCurrency);
}
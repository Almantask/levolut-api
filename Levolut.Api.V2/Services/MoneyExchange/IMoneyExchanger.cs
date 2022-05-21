using Levolut.Api.V2.Database.Models;

namespace Levolut.Api.V2.Services.MoneyExchange;

public interface IMoneyExchanger
{
    decimal Exchange(long bankId, Models.MoneyExchange moneyExchange, Currency fromCurrency);
}
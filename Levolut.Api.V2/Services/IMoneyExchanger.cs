using Levolut.Api.V2.Infrastructure.Database.Models;

namespace Levolut.Api.V2.Controllers;

public interface IMoneyExchanger
{
    decimal Exchange(long bankId, MoneyExchange moneyExchange, Currency fromCurrency);
}
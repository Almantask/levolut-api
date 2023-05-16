using Levolut.Api.V3.Domain.Models;

namespace Levolut.Api.V3.Domain.Services.MoneyExchange;

public interface IMoneyExchanger
{
    decimal Exchange(long bankId, Models.ValueObjects.MoneyExchange moneyExchange, Currency fromCurrency);
}
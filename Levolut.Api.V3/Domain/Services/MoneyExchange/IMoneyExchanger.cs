using Levolut.Api.V3.Infrastructure.Database.Entities;

namespace Levolut.Api.V3.Domain.Services.MoneyExchange;

public interface IMoneyExchanger
{
    decimal Exchange(long bankId, Models.MoneyExchange moneyExchange, Currency fromCurrency);
}
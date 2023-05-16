using Levolut.Api.V3.Infrastructure.Database.Entities;

namespace Levolut.Api.V3.Domain.Services.MoneyExchange
{
    public interface ICurrencyProvider
    {
        decimal GetRate(Currency currency);
    }
}
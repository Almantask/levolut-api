using Levolut.Api.V2.Database.Models;

namespace Levolut.Api.V2.Services.MoneyExchange
{
    public interface ICurrencyProvider
    {
        decimal GetRate(Currency currency);
    }
}
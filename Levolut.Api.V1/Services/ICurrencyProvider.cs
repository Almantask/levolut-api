using Levolut.Api.V1.Infrastructure.Database.Models;

namespace Levolut.Api.V1.Services
{
    public interface ICurrencyProvider
    {
        decimal GetRate(Currency currency);
    }
}
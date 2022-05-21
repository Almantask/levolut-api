using Levolut.Api.V2.Infrastructure.Database.Models;

namespace Levolut.Api.V2.Services.Interfaces
{
    public interface ICurrencyProvider
    {
        decimal GetRate(Currency currency);
    }
}
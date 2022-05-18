using LevolutApi.Infrastructure.Database;

namespace LevolutApi.Services
{
    public interface ICurrencyProvider
    {
        decimal GetRate(Currency currency);
    }
}
using Levolut.Api.V2.Database.Entities;

namespace Levolut.Api.V2.Services.MoneyExchange
{
    public class CurrencyProvider : ICurrencyProvider
    {
        public decimal GetRate(Currency currency)
        {
            switch (currency)
            {
                case Currency.EUR:
                    return 1;
                case Currency.USD:
                    return 1.05m;
                case Currency.GBP:
                    return 1.18m;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

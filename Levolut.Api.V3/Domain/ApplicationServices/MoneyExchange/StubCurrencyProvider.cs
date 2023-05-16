using Levolut.Api.V3.Domain.Models;

namespace Levolut.Api.V3.Domain.Services.MoneyExchange
{
    public class StubCurrencyProvider : ICurrencyProvider
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

using Levolut.Api.V3.Domain.Models;
using Levolut.Api.V3.Domain.Services.MoneyExchange;

namespace Levolut.Api.V3.Infrastructure.Adapters.CurrencyProvider
{
    // https://fixer.io/documentation#latestrates
    public class ArcaPayCurrentProviderAdapter : ICurrencyProvider
    {
        private readonly HttpClient _httpClient;

        public ArcaPayCurrentProviderAdapter(HttpClient client)
        {
            _httpClient = client;
        }

        public decimal GetRate(Currency currency)
        {
            var query = $@"https://data.fixer.io/api/latest
    ? access_key = XXXXYYYYYYYYYZ
    & base = {Currency.EUR}
    & symbols = {currency}";
            var response = _httpClient.GetAsync(query).GetAwaiter().GetResult();

            response.EnsureSuccessStatusCode();

            var latestCurrencyRate = response.Content.ReadAsAsync<LatestCurrencyRateResponse>().GetAwaiter().GetResult();

            return latestCurrencyRate.rates[currency.ToString()];
        }
    }
}

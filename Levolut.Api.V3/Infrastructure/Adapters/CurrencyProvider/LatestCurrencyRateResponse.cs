namespace Levolut.Api.V3.Infrastructure.Adapters.CurrencyProvider
{
    public class LatestCurrencyRateResponse
    {
        public bool success { get; set; }
        public int timestamp { get; set; }
        public string @base { get; set; }
        public string date { get; set; }
        public Dictionary<string,decimal> rates { get; set; }
    }
}

namespace Levolut.Api.V1.Contracts
{
    public class AddBankFeeRuleRequest
    {
        public decimal Fee { get; set; }
        public IEnumerable<string> BlockedCountries { get; set; }
    }
}
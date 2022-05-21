namespace Levolut.Api.V2.Contracts.Requests
{
    public record AddBankFeeRuleRequest(decimal Fee, IEnumerable<string> BlockedCountries);
}
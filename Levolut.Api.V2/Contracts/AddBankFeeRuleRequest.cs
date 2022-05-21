namespace Levolut.Api.V2.Contracts
{
    public record AddBankFeeRuleRequest(decimal Fee, IEnumerable<string> BlockedCountries);
}
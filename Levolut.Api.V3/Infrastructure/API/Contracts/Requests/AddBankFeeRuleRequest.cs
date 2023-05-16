namespace Levolut.Api.V3.Infrastructure.API.Contracts.Requests
{
    public record AddBankFeeRuleRequest(decimal Fee, IEnumerable<string> BlockedCountries);
}
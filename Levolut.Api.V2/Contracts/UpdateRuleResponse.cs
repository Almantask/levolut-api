using Levolut.Api.V2.Infrastructure.Database.Models;

namespace Levolut.Api.V2.Contracts
{
    internal class GetBankFeeRulesResponse
    {
        public BankFeeRule BankFeeRule { get; internal set; }
    }
}
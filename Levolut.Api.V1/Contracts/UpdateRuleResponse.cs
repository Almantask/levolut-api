using Levolut.Api.V1.Infrastructure.Database.Models;

namespace Levolut.Api.V1.Contracts
{
    internal class GetBankFeeRulesResponse
    {
        public BankFeeRule BankFeeRule { get; internal set; }
    }
}
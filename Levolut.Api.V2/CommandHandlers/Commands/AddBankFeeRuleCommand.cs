using Levolut.Api.V2.Infrastructure.Database.Models;

namespace Levolut.Api.V2.CommandHandlers.Commands
{
    public class AddBankFeeRuleCommand
    {
        public BankFeeRule BankFeeRule { get; }

        public AddBankFeeRuleCommand(BankFeeRule bankFeeRule)
        {
            BankFeeRule = bankFeeRule;
        }
    }
}
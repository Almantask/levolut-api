using Levolut.Api.V2.Infrastructure.Database.Models;

namespace Levolut.Api.V2.Controllers
{
    // Why do we need a service? Why can't we call db directly or at least why can't we call handler directly?
    public class BankFeeService : IBankFeeService
    {
        private readonly IQueryHandler<GetRuleQuery, BankFeeRule> getRuleQueryHandler;
        private readonly ICommandHandler<AddBankFeeRuleCommand, BankFeeRule> addBankFeeRuleCommandHandler;

        public BankFeeService(IQueryHandler<GetRuleQuery, BankFeeRule> getRuleQueryHandler, ICommandHandler<AddBankFeeRuleCommand, BankFeeRule> addBankFeeRuleCommandHandler)
        {
            this.getRuleQueryHandler = getRuleQueryHandler;
            this.addBankFeeRuleCommandHandler = addBankFeeRuleCommandHandler;
        }

        public BankFeeRule AddBankFeeRule(BankFeeRule bankFeeRule)
        {
            var addedBankFeeRule = addBankFeeRuleCommandHandler.Handle(new AddBankFeeRuleCommand(bankFeeRule));
            return addedBankFeeRule;
        }

        public BankFeeRule GetBankFeeRule(long bankId)
        {
            var bankFeeRule = getRuleQueryHandler.Handle(new GetRuleQuery { BankId = bankId });

            if (bankFeeRule == null)
            {
                throw new EntityNotFoundException("Bank or BankFeee");
            }

            return bankFeeRule;
        }
    }
}
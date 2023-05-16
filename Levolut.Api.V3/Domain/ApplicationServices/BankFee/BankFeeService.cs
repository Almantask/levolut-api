using Levolut.Api.V3.Domain.DomainServices.Command;
using Levolut.Api.V3.Domain.DomainServices.Query;
using Levolut.Api.V3.Domain.Exceptions;
using Levolut.Api.V3.Domain.Models.Entities;

namespace Levolut.Api.V3.Domain.Services.BankFee
{
    // Why do we need a service? Why can't we call db directly or at least why can't we call handler directly?
    public class BankFeeService : IBankFeeService
    {
        private readonly IQueryHandler<GetBankFeeRuleQuery, BankFeeRule> getRuleQueryHandler;
        private readonly ICommandHandler<AddBankFeeRuleCommand, BankFeeRule> addBankFeeRuleCommandHandler;

        public BankFeeService(IQueryHandler<GetBankFeeRuleQuery, BankFeeRule> getRuleQueryHandler, ICommandHandler<AddBankFeeRuleCommand, BankFeeRule> addBankFeeRuleCommandHandler)
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
            var bankFeeRule = getRuleQueryHandler.Handle(new GetBankFeeRuleQuery(bankId));

            if (bankFeeRule == null)
            {
                throw new EntityNotFoundException("Bank or BankFeee");
            }

            return bankFeeRule;
        }
    }
}
using Levolut.Api.V2.CommandHandlers;
using Levolut.Api.V2.CommandHandlers.Commands;
using Levolut.Api.V2.Controllers;
using Levolut.Api.V2.Exceptions;
using Levolut.Api.V2.Infrastructure.Database.Models;
using Levolut.Api.V2.QueryHandlers;
using Levolut.Api.V2.QueryHandlers.Queries;
using Levolut.Api.V2.Services.Interfaces;

namespace Levolut.Api.V2.Services
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
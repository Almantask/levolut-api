using Levolut.Api.V3.Domain.DomainServices.Command;
using Levolut.Api.V3.Domain.Models.Entities;

namespace Levolut.Api.V3.Infrastructure.Database.Handlers.Command
{
    public class AddBankFeeRuleCommandHandler : ICommandHandler<AddBankFeeRuleCommand, BankFeeRule>
    {
        private readonly LevolutDbContext db;

        public AddBankFeeRuleCommandHandler(LevolutDbContext db)
        {
            this.db = db;
        }

        public BankFeeRule Handle(AddBankFeeRuleCommand addBankFeeRuleCommand)
        {
            var entityEntry = db.BankFeeRules.Add(addBankFeeRuleCommand.BankFeeRule);

            db.SaveChanges();

            return entityEntry.Entity;
        }
    }
}
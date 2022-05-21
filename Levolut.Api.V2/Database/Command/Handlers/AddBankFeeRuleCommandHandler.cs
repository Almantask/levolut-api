using Levolut.Api.V2.Database.Command.Commands;
using Levolut.Api.V2.Database.Models;

namespace Levolut.Api.V2.Database.Command.Handlers
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
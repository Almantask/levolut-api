using Levolut.Api.V2.Infrastructure.Database;
using Levolut.Api.V2.Infrastructure.Database.Models;

namespace Levolut.Api.V2.Controllers
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
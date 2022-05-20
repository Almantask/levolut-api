using Levolut.Api.V2.Infrastructure.Database;
using Levolut.Api.V2.Infrastructure.Database.Models;
using Levolut.Api.V2.QueryHandlers.Queries;
using Microsoft.EntityFrameworkCore;

namespace Levolut.Api.V2.QueryHandlers
{
    // Question about Db context abstraction.
    public class GetBankFeeRuleQueryHandler : IQueryHandler<GetRuleQuery, BankFeeRule>
    {
        private readonly LevolutDbContext db;

        public GetBankFeeRuleQueryHandler(LevolutDbContext db)
        {
            this.db = db;
        }

        public BankFeeRule Handle(GetRuleQuery getRuleQuery)
        {
            var bankFeeRule = db.BankFeeRules
                .Include(i => i.BlockedCountries)
                .FirstOrDefault(bfr => bfr.BankId == getRuleQuery.BankId);

            return bankFeeRule;
        }
    }
}
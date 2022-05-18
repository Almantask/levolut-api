using Levolut.Api.V2.Infrastructure.Database;
using Levolut.Api.V2.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Levolut.Api.V2.Controllers
{
    // LSP and ISP compliant.
    // Why is it better than repo?
    // API versioning when?

    // Question about having generic vs non-generic handlers
    // The benefit of this vs repository pattern.
    public interface IQueryHandler<TQuery, TResponse> 
        where TQuery : class
        where TResponse: class
    {
        TResponse Handle(TQuery getRuleQuery);
    }

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
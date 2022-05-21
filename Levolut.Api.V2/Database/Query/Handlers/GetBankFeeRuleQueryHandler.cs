using Levolut.Api.V2.Database.Models;
using Levolut.Api.V2.Database.Query.Queries;
using Levolut.Api.V2.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Levolut.Api.V2.Database.Query.Handlers;

public class GetBankFeeRuleQueryHandler : IQueryHandler<GetBankFeeRuleQuery, BankFeeRule>
{
    private readonly LevolutDbContext context;

    public GetBankFeeRuleQueryHandler(LevolutDbContext context)
    {
        this.context = context;
    }

    public BankFeeRule Handle(GetBankFeeRuleQuery query)
    {
        BankFeeRule bankFeeRule;
        try
        {
            bankFeeRule = context.BankFeeRules
                .Include(s => s.BlockedCountries)
                .First(banckFeeRule => banckFeeRule.BankId == query.BankId);
            return bankFeeRule;
        }
        catch (InvalidOperationException)
        {
            throw new EntityNotFoundException("Bank fee rule");
        }
    }
}
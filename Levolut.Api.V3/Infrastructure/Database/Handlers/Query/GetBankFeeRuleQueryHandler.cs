using Levolut.Api.V3.Domain.Exceptions;
using Levolut.Api.V3.Domain.Query;
using Levolut.Api.V3.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Levolut.Api.V3.Infrastructure.Database.Query.Handlers;

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
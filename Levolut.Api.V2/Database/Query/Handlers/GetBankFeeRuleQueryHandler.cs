using Levolut.Api.V2.Exceptions;
using Levolut.Api.V2.Infrastructure.Database;
using Levolut.Api.V2.Infrastructure.Database.Models;
using Levolut.Api.V2.QueryHandlers;
using Microsoft.EntityFrameworkCore;

namespace Levolut.Api.V2.Controllers;

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
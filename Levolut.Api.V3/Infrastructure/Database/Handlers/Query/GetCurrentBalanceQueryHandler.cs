using Levolut.Api.V3.Domain.DomainServices.Query;
using Levolut.Api.V3.Domain.Models.Entities;

namespace Levolut.Api.V3.Infrastructure.Database.Query.Handlers;

public class GetCurrentBalanceQueryHandler : IQueryHandler<GetCurrentBalanceQuery, Balance>
{
    private readonly LevolutDbContext _context;

    public GetCurrentBalanceQueryHandler(LevolutDbContext context)
    {
        _context = context;
    }

    public Balance Handle(GetCurrentBalanceQuery query)
    {
        // Why didn't we throw an exception when balance wasn't found here?
        var balance = _context.Balances
            .Where(b => b.UserId == query.UserId && b.BankId == query.BankId)
            .OrderByDescending(b => b.CreatedAt)
            .FirstOrDefault();

        return balance;
    }
}
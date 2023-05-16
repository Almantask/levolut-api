using Levolut.Api.V3.Domain.Command;
using Levolut.Api.V3.Infrastructure.Database.Entities;

namespace Levolut.Api.V3.Infrastructure.Database.Handlers.Command;

public class AddBalanceCommandHandler : ICommandHandler<AddBalanceCommand, Balance>
{
    private readonly LevolutDbContext _context;

    public AddBalanceCommandHandler(LevolutDbContext context)
    {
        _context = context;
    }

    public Balance Handle(AddBalanceCommand command)
    {
        var balanceEntityEntry = _context.Balances.Add(command.Balance);
        _context.SaveChanges();

        return balanceEntityEntry.Entity;
    }
}
using Levolut.Api.V2.Database.Command.Commands;
using Levolut.Api.V2.Database.Entities;

namespace Levolut.Api.V2.Database.Command.Handlers;

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
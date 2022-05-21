using Levolut.Api.V2.CommandHandlers;
using Levolut.Api.V2.Infrastructure.Database;
using Levolut.Api.V2.Infrastructure.Database.Models;

namespace Levolut.Api.V2.Controllers;

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
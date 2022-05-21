using Levolut.Api.V2.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Levolut.Api.V2.Database
{
    public class LevolutDbContext : DbContext
    {
        public LevolutDbContext(DbContextOptions<LevolutDbContext> options) : base(options)
        {

        }
        public DbSet<BankFeeRule> BankFeeRules { get; set; }
        public DbSet<Balance> Balances { get; set; }
    }
}

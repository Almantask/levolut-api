using Levolut.Api.V3.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Levolut.Api.V3.Infrastructure.Database
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

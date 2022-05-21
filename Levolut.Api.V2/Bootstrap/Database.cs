using Levolut.Api.V2.Database;
using Microsoft.EntityFrameworkCore;

namespace Levolut.Api.V2.Bootstrap
{
    public static class Database
    {
        public static IServiceCollection AddLevolutDatabase(this IServiceCollection services)
        {
            services.AddDbContext<LevolutDbContext>(options => options.UseInMemoryDatabase("Levolut"));
            return services;
        }
    }
}

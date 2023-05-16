using Levolut.Api.V3.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Levolut.Api.V3.Infrastructure.API.Bootstrap
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

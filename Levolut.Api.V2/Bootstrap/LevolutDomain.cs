using Levolut.Api.V2.Controllers;
using Levolut.Api.V2.Infrastructure.Database.Models;
using Levolut.Api.V2.Services;

namespace Levolut.Api.V2.Bootstrap
{
    public static class LevolutDomain
    {
        public static IServiceCollection AddLevolutDomain(this IServiceCollection services)
        {
            services.AddScoped<IBankFeeService, BankFeeService>();
            // Mention that we could use a base class here in order to simplify DI and escape repetitive context injection.
            services.AddScoped<IQueryHandler<GetRuleQuery, BankFeeRule>, GetBankFeeRuleQueryHandler>();
            services.AddScoped<ICommandHandler<AddBankFeeRuleCommand, BankFeeRule>, AddBankFeeRuleCommandHandler>();
            services.AddSingleton<ICurrencyProvider, CurrencyProvider>();
            return services;
        }
    }
}

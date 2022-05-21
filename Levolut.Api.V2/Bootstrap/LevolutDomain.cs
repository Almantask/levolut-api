using Levolut.Api.V2.CommandHandlers;
using Levolut.Api.V2.CommandHandlers.Commands;
using Levolut.Api.V2.Controllers;
using Levolut.Api.V2.Infrastructure.Database.Models;
using Levolut.Api.V2.QueryHandlers;
using Levolut.Api.V2.QueryHandlers.Queries;
using Levolut.Api.V2.Services;
using Levolut.Api.V2.Services.Interfaces;

namespace Levolut.Api.V2.Bootstrap
{
    public static class LevolutDomain
    {
        public static IServiceCollection AddLevolutDomain(this IServiceCollection services)
        {
            services.AddScoped<IBankFeeService, BankFeeService>();
            // Mention that we could use a base class here in order to simplify DI and escape repetitive context injection.
            services.AddScoped<IQueryHandler<GetBankFeeRuleQuery, BankFeeRule>, GetBankFeeRuleQueryHandler>();
            services.AddScoped<IQueryHandler<GetCurrentBalanceQuery, Balance>, GetCurrentBalanceQueryHandler>();
            services.AddScoped<ICommandHandler<AddBankFeeRuleCommand, BankFeeRule>, AddBankFeeRuleCommandHandler>();
            services.AddScoped<ICommandHandler<AddBalanceCommand, Balance>, AddBalanceCommandHandler>();
            services.AddSingleton<ICurrencyProvider, CurrencyProvider>();
            services.AddScoped<IMoneyExchanger, MoneyExchanger>();
            services.AddScoped<IMoney>
            return services;
        }
    }
}

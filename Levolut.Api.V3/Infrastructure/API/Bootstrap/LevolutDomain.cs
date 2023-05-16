using Levolut.Api.V3.Domain.Command;
using Levolut.Api.V3.Domain.Models.Entities;
using Levolut.Api.V3.Domain.Query;
using Levolut.Api.V3.Domain.Services.Balance;
using Levolut.Api.V3.Domain.Services.BankFee;
using Levolut.Api.V3.Domain.Services.MoneyExchange;
using Levolut.Api.V3.Infrastructure.Database.Handlers.Command;
using Levolut.Api.V3.Infrastructure.Database.Query.Handlers;

namespace Levolut.Api.V3.Infrastructure.API.Bootstrap
{
    public static class LevolutDomain
    {
        public static IServiceCollection AddLevolutDomain(this IServiceCollection services)
        {
            services.AddScoped<IBankFeeService, BankFeeService>();
            // Mention that we could use a base class here in order to simplify DI and escape repetitive context injection.
            // What are the tradeoffs?
            services.AddScoped<IQueryHandler<GetBankFeeRuleQuery, BankFeeRule>, GetBankFeeRuleQueryHandler>();
            services.AddScoped<IQueryHandler<GetCurrentBalanceQuery, Balance>, GetCurrentBalanceQueryHandler>();
            services.AddScoped<ICommandHandler<AddBankFeeRuleCommand, BankFeeRule>, AddBankFeeRuleCommandHandler>();
            services.AddScoped<ICommandHandler<AddBalanceCommand, Balance>, AddBalanceCommandHandler>();
            services.AddScoped<IMoneyExchanger, MoneyExchanger>();
            services.AddScoped<IBalanceService, BalanceService>();

            services.AddSingleton<ICurrencyProvider, StubCurrencyProvider>();
            return services;
        }
    }
}

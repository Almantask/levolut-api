using Hellang.Middleware.ProblemDetails;
using Levolut.Api.V3.Domain.Exceptions;

namespace Levolut.Api.V3.Infrastructure.API.Bootstrap
{
    public static class ErrorHandling
    {
        public static IServiceCollection AddErrorHandling(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddProblemDetails(setup =>
            {
                setup.IncludeExceptionDetails = (_, _) => environment.IsDevelopment();
                setup.MapToStatusCode<EntityNotFoundException>(404);
                setup.MapToStatusCode<BlockedCountryException>(403);
            });

            return services;
        }
    }
}

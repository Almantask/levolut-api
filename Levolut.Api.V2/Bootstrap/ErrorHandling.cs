using Hellang.Middleware.ProblemDetails;
using Levolut.Api.V2.Exceptions;

namespace Levolut.Api.V2.Bootstrap
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

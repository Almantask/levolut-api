using Hellang.Middleware.ProblemDetails;
using Levolut.Api.V2.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Levolut.Api.V2.Bootstrap
{
    public static class ErrorHandling
    {
        public static IServiceCollection AddErrorHandling(this IServiceCollection services)
        {
            services.AddProblemDetails(setup =>
            {
                setup.MapToStatusCode<EntityNotFoundException>(404);
            });

            return services;
        }
    }
}

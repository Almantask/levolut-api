namespace Levolut.Api.V2.Bootstrap
{
    public static class ControllerConfiguration
    {
        public static IServiceCollection AddControllerConfiguration(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddControllers().AddNewtonsoftJson();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            return services;
        }
    }
}

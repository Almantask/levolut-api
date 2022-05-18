namespace Levolut.Api.V2.Bootstrap
{
    public static class ControllerConfiguration
    {
        public static IServiceCollection AddControllerConfiguration(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            return services;
        }
    }
}

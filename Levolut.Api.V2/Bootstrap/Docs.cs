namespace Levolut.Api.V2.Bootstrap
{
    public static class Docs
    {
        public static IServiceCollection AddDocs(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}

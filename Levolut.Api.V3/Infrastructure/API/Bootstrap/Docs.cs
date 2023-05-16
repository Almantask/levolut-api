using Microsoft.OpenApi.Models;

namespace Levolut.Api.V3.Infrastructure.API.Bootstrap
{
    public static class Docs
    {
        public static IServiceCollection AddDocs(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options => options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = "Levolut API",
                Contact = new OpenApiContact
                {
                    Name = "Levolut Team",
                    Email = "team@levolut.com",
                    Url = new Uri("https://levolut.com/contact")
                }
            }));

            return services;
        }
    }
}

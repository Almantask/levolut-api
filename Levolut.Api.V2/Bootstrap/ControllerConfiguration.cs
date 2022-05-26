using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

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

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            //Also can be enriched with:
            //services.AddHsts()
            //services.AddDefaultCorrelationId()
            //services.AddCors()

            return services;
        }
    }
}

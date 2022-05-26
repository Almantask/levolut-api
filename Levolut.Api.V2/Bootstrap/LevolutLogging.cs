using Serilog;

namespace Levolut.Api.V2.Bootstrap
{
    public static class LevolutLogging
    {
        //Enrich logging with exstra details
        //Extend and control logging
        public static void AddLevolutLogging(this IServiceCollection services, IWebHostEnvironment environment)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug();

            if (environment.IsDevelopment())
            {
                loggerConfiguration.WriteTo
                    .Console();
            }
            else
            {
                //Possible to extend with Application insights
                loggerConfiguration.WriteTo
                    .File("logs/levolut.txt", rollingInterval: RollingInterval.Day);
            }

            var logger = loggerConfiguration
                .CreateLogger();

            services.AddLogging(configure =>
            {
                configure.AddSerilog(logger);
            });
        }
    }
}

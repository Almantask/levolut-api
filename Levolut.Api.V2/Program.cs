using Hellang.Middleware.ProblemDetails;
using Levolut.Api.V2.Bootstrap;

var builder = WebApplication.CreateBuilder(args);
SetupServices(builder.Services, builder.Environment);

var app = builder.Build();
SetupMiddleware(app);

app.Run();

static void SetupServices(IServiceCollection services, IWebHostEnvironment environment)
{
    services.AddControllerConfiguration();
    services.AddDocs();
    services.AddLevolutLogging(environment);
    services.AddLevolutDatabase();
    services.AddErrorHandling(environment);
    services.AddLevolutDomain();
}

static void SetupMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
        });
    }

    app.UseProblemDetails();

    app.UseAuthorization();

    app.MapControllers();
}
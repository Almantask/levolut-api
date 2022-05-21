using Levolut.Api.V1.Infrastructure.Database;
using Levolut.Api.V1.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
SetupServices(builder.Services);

var app = builder.Build();
SetupMiddleware(app);
app.Run();

static void SetupServices(IServiceCollection services)
{
    services.AddDbContext<LevolutDbContext>(options => options.UseInMemoryDatabase("Levolut"));
    services.AddSingleton<ICurrencyProvider, CurrencyProvider>();
    services.AddControllers().AddNewtonsoftJson();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

static void SetupMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();
}



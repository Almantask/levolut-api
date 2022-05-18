using Hellang.Middleware.ProblemDetails;
using Levolut.Api.V2.Bootstrap;
using Levolut.Api.V2.Infrastructure.Database;
using Levolut.Api.V2.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddControllerConfiguration();
services.AddDocs();
services.AddLevolutDatabase();
services.AddErrorHandling();
services.AddLevolutDomain();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseProblemDetails();

app.UseAuthorization();

app.MapControllers();

app.Run();

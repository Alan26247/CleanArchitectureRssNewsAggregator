using Infrastructure.Data;
using Web.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddServices(builder.Configuration);

var app = builder.Build();

var scopeService = app.Services.CreateScope();
await scopeService.ServiceProvider.GetRequiredService<AutoMigrator>().Run();

app.UseMiddlewares();

app.Run();

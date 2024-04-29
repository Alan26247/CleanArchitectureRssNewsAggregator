using Core.Common.Interfaces.IRepositories;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Common.Extensions;
public static class InfrastructureExtension
{
    public static void AddInfrastructure(this IServiceCollection services, string dbConnectionString)
    {
        // ----- database -----
        services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(dbConnectionString);
            options.UseSnakeCaseNamingConvention();
        });

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(dbConnectionString);
            options.UseSnakeCaseNamingConvention();
        });

        // ----- helpers -----
        services.AddScoped<ISqlRangeQueryHelper, SqlRangeQueryHelper>();

        // ----- repositories ------
        services.AddScoped<IChannelRepository, ChannelRepository>();
        services.AddScoped<INewsRepository, NewsRepository>();
    }
}

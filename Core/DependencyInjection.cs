using Core.Common.Interfaces.IServices;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            // ----- services -----
            services.AddHostedService<BackgroundWorkerService>();

            services.AddScoped<IUpdateChannelNewsService, UpdateChannelNewsService>();
            services.AddScoped<IConvertorXmlToRssObjectService, ConvertorXmlToRssObjectService>();

            return services;
        }
    }
}

using Core;
using Core.Common.Extensions;
using Infrastructure.Data;

namespace Web.Common.Extensions
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddApplication();

            var connectionString = configuration.GetConnectionString("Data");

            services.AddInfrastructure(connectionString!);

            services.AddMapper();

            services.AddSwagger();

            services.AddCorsPolitics();

            services.AddTransient<AutoMigrator>();

            services.AddHttpClient();
        }
    }
}
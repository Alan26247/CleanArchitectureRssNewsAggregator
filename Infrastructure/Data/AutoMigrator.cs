using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class AutoMigrator
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AutoMigrator> _logger;

        public AutoMigrator(IConfiguration configuration, ApplicationDbContext context, ILogger<AutoMigrator> logger)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
        }

        public async Task Run()
        {
            //_logger.LogInformation("Environment - {Environment}", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            // _logger.LogInformation("DbConnectString - {Data}", _configuration.GetConnectionString("Data"));

            await _context.Database.MigrateAsync();
        }
    }
}

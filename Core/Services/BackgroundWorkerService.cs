using Core.Commands.ChannelCommands;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Services;

public class BackgroundWorkerService : BackgroundService
{
    private readonly PeriodicTimer _timer;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<BackgroundWorkerService> _logger;
    public BackgroundWorkerService(IConfiguration configuration, IServiceProvider serviceProvider, ILogger<BackgroundWorkerService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _timer = new(TimeSpan.FromMinutes(int.Parse(configuration["BackgroundTaskPeriodMinutes"]!)));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Запущен фоновый сервис");

                using IServiceScope scope = _serviceProvider.CreateScope();

                // ----- обновляем новости каналов -----
                var mediaror = scope.ServiceProvider.GetRequiredService<IMediator>();

                var command = new UpdateChannelsNewsCommand();

                await mediaror.Send(command);

                _logger.LogInformation("Новости каналов обновлены");

                _logger.LogInformation("Фоновый сервис успешно завершил работу");

            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка фонового сервиса: {ex}", ex);
            }
        }
    }
}
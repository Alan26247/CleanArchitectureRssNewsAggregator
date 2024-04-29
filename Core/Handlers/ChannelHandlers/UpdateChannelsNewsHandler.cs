using Core.Commands.ChannelCommands;
using Core.Common.Interfaces.IRepositories;
using Core.Common.Interfaces.IServices;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Handlers.ChannelHandlers
{
    public class UpdateChannelsNewsHandler : IRequestHandler<UpdateChannelsNewsCommand>
    {
        private readonly IChannelRepository _channelRepository;
        private readonly INewsRepository _newsRepository;
        private readonly IUpdateChannelNewsService _updateChannelNewsService;
        private readonly ILogger<UpdateChannelsNewsHandler> _logger;

        public UpdateChannelsNewsHandler(IChannelRepository channelRepository, INewsRepository newsRepository,
            IUpdateChannelNewsService updateChannelNewsService, ILogger<UpdateChannelsNewsHandler> logger)
        {
            _channelRepository = channelRepository;
            _newsRepository = newsRepository;
            _updateChannelNewsService = updateChannelNewsService;
            _logger = logger;
        }

        public async Task Handle(UpdateChannelsNewsCommand request, CancellationToken cancellationToken)
        {
            var channels = await _channelRepository.GetListAsync();

            foreach (var channel in channels)
            {
                try
                {
                    var channelResult = await _updateChannelNewsService.UpdateAsync(channel);

                    await _channelRepository.UpdateChannelNewsAsync(channelResult);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Ошибка обновления канала ({channel.Title}): ", ex.Message);
                }
            }

            await _newsRepository.ClearOldNewsAsync();
        }
    }
}

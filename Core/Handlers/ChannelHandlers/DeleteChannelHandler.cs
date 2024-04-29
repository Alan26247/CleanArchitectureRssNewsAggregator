using Core.Commands.ChannelCommands;
using Core.Common.Exceptions;
using Core.Common.Interfaces.IRepositories;
using MediatR;

namespace Core.Handlers.ChannelHandlers
{
    public class DeleteChannelHandler : IRequestHandler<DeleteChannelCommand>
    {
        private readonly IChannelRepository _channelRepository;

        public DeleteChannelHandler(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public async Task Handle(DeleteChannelCommand request, CancellationToken cancellationToken)
        {
            var currentChannel = await _channelRepository.GetAsync(request.Id);

            if (currentChannel == null) throw new AppException(400, "Channel not found.");

            if (currentChannel.IsFixed) throw new AppException(400, "Channel is protected.");

            await _channelRepository.DeleteAsync(currentChannel);
        }
    }
}

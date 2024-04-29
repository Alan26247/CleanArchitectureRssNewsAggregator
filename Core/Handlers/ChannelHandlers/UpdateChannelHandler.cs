using AutoMapper;
using Core.Commands.ChannelCommands;
using Core.Common.Exceptions;
using Core.Common.Interfaces.IRepositories;
using Core.Common.Interfaces.IServices;
using MediatR;

namespace Core.Handlers.ChannelHandlers
{
    public class UpdateChannelHandler : IRequestHandler<UpdateChannelCommand>
    {
        private readonly IMapper _mapper;
        private readonly IChannelRepository _channelRepository;
        private readonly IUpdateChannelNewsService _updateChannelNewsService;

        public UpdateChannelHandler(IMapper mapper, IChannelRepository channelRepository, IUpdateChannelNewsService updateChannelNewsService)
        {
            _mapper = mapper;
            _channelRepository = channelRepository;
            _updateChannelNewsService = updateChannelNewsService;
        }

        public async Task Handle(UpdateChannelCommand request, CancellationToken cancellationToken)
        {
            var currentChannel = await _channelRepository.GetAsync(request.Id);

            if (currentChannel == null) throw new AppException(400, "Channel not found.");

            _mapper.Map(request, currentChannel);

            await _channelRepository.UpdateAsync(currentChannel);
        }
    }
}

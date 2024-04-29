using AutoMapper;
using Core.Commands.ChannelCommands;
using Core.Common.Interfaces.IRepositories;
using Core.Common.Interfaces.IServices;
using MediatR;

namespace Core.Handlers.ChannelHandlers
{
    public class AddChannelHandler : IRequestHandler<AddChannelCommand, long>
    {
        private readonly IMapper _mapper;
        private readonly IChannelRepository _channelRepository;
        private readonly IUpdateChannelNewsService _updateChannelNewsService;

        public AddChannelHandler(IMapper mapper, IChannelRepository channelRepository, IUpdateChannelNewsService updateChannelNewsService)
        {
            _mapper = mapper;
            _channelRepository = channelRepository;
            _updateChannelNewsService = updateChannelNewsService;
        }

        public async Task<long> Handle(AddChannelCommand request, CancellationToken cancellationToken)
        {
            var newChannel = _mapper.Map<Common.Entities.Channel>(request);

            newChannel = await _updateChannelNewsService.UpdateAsync(newChannel);

            var id = await _channelRepository.AddAsync(newChannel);

            return id;
        }
    }
}

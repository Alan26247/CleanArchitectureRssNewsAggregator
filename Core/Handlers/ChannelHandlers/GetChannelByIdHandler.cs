using AutoMapper;
using Core.Common.Dtos.ChannelDtos;
using Core.Common.Interfaces.IRepositories;
using Core.Queries.ChannelQueries;
using MediatR;

namespace Core.Handlers.ChannelHandlers
{
    public class GetChannelByIdHandler : IRequestHandler<GetChannelByIdQuery, ChannelDto?>
    {
        private readonly IMapper _mapper;
        private readonly IChannelRepository _channelRepository;

        public GetChannelByIdHandler(IMapper mapper, IChannelRepository channelRepository)
        {
            _mapper = mapper;
            _channelRepository = channelRepository;
        }

        public async Task<ChannelDto?> Handle(GetChannelByIdQuery request, CancellationToken cancellationToken)
        {
            var channel = await _channelRepository.GetAsync(request.Id);

            if (channel == null) return null;

            return _mapper.Map<ChannelDto>(channel);
        }
    }
}

using AutoMapper;
using Core.Common.Dtos.ChannelDtos;
using Core.Common.Dtos.CommonDtos;
using Core.Common.Interfaces.IRepositories;
using Core.Queries.ChannelQueries;
using MediatR;

namespace Core.Handlers.ChannelHandlers
{
    public class GetChannelListHandler : IRequestHandler<GetChannelListQuery, DataWithPaginationDto<List<ChannelDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IChannelRepository _channelRepository;

        public GetChannelListHandler(IMapper mapper, IChannelRepository channelRepository)
        {
            _mapper = mapper;
            _channelRepository = channelRepository;
        }

        public async Task<DataWithPaginationDto<List<ChannelDto>>> Handle(GetChannelListQuery request, CancellationToken cancellationToken)
        {
            var response = await _channelRepository.GetChannelsAsync(request.FindString, request.PageNumber, request.PageSize);

            return _mapper.Map<DataWithPaginationDto<List<ChannelDto>>>(response);
        }
    }
}

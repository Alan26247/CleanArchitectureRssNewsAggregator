using AutoMapper;
using Core.Common.Dtos.CommonDtos;
using Core.Common.Dtos.NewsDtos;
using Core.Common.Interfaces.IRepositories;
using Core.Queries.NewsQueries;
using MediatR;

namespace Core.Handlers.NewsHandlers
{
    public class GetNewsListHandler : IRequestHandler<GetNewsListQuery, DataWithPaginationDto<List<NewsDto>>>
    {
        private readonly IMapper _mapper;
        private readonly INewsRepository _newsRepository;

        public GetNewsListHandler(IMapper mapper, INewsRepository newsRepository)
        {
            _mapper = mapper;
            _newsRepository = newsRepository;
        }

        public async Task<DataWithPaginationDto<List<NewsDto>>> Handle(GetNewsListQuery request, CancellationToken cancellationToken)
        {
            var response = await _newsRepository.GetNewsAsync(request.FindString, request.ChannelId, request.PageNumber, request.PageSize);

            return _mapper.Map<DataWithPaginationDto<List<NewsDto>>>(response);
        }
    }
}

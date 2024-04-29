using Core.Common.Dtos.CommonDtos;
using Core.Common.Dtos.NewsDtos;
using MediatR;

namespace Core.Queries.NewsQueries;

public class GetNewsListQuery : IRequest<DataWithPaginationDto<List<NewsDto>>>
{
    public string? FindString { get; set; }
    public int? ChannelId { get; set; }

    public int? PageNumber { get; set; } = 1;
    public int? PageSize { get; set; } = 20;
}
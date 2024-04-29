using Core.Common.Dtos.ChannelDtos;
using Core.Common.Dtos.CommonDtos;
using MediatR;

namespace Core.Queries.ChannelQueries;

public class GetChannelListQuery : IRequest<DataWithPaginationDto<List<ChannelDto>>>
{
    public string? FindString { get; set; }

    public int? PageNumber { get; set; } = 1;
    public int? PageSize { get; set; } = 20;
}
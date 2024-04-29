using Core.Common.Dtos.ChannelDtos;
using MediatR;

namespace Core.Queries.ChannelQueries;

public class GetChannelByIdQuery : IRequest<ChannelDto?>
{
    public long Id { get; set; }
}
using MediatR;

namespace Core.Commands.ChannelCommands;

public class AddChannelCommand : IRequest<long>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string RssLink { get; set; } = string.Empty;
}
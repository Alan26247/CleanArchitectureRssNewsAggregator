using MediatR;

namespace Core.Commands.ChannelCommands;

public class UpdateChannelCommand : IRequest
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
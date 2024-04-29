using MediatR;

namespace Core.Commands.ChannelCommands;

public class DeleteChannelCommand : IRequest
{
    public long Id { get; set; }
}
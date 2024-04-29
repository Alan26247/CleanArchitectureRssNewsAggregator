using Core.Commands.ChannelCommands;
using Core.Queries.ChannelQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Web.Common.Extensions;

namespace Web.Controllers
{
    [Route("api/channel")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        public ChannelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;


        [HttpPost("")]
        [SwaggerOperation(
                Summary = "Добавить канал",
                Description = "Добавить канал",
                OperationId = "Channel.Post",
                Tags = new[] { "Channel" }
            )]
        public async Task<IActionResult> AddChannel([FromBody] AddChannelCommand command)
        {
            var response = await _mediator.Send(command);

            return this.GetResponse(201, response);
        }

        [HttpPut("")]
        [SwaggerOperation(
                Summary = "Обновить канал",
                Description = "Обновить канал",
                OperationId = "Channel.Put",
                Tags = new[] { "Channel" }
            )]
        public async Task<IActionResult> UpdateChannel([FromBody] UpdateChannelCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
                Summary = "удалить канал",
                Description = "Удалить канал",
                OperationId = "Channel.Delete",
                Tags = new[] { "Channel" }
            )]
        public async Task<IActionResult> DeleteChannel([FromRoute] long id)
        {
            var command = new DeleteChannelCommand { Id = id };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
                Summary = "Получить канал по id",
                Description = "Получить канал по id",
                OperationId = "Channel.Get",
                Tags = new[] { "Channel" }
            )]
        public async Task<IActionResult> GetChannelById([FromRoute] long id)
        {
            var query = new GetChannelByIdQuery { Id = id };

            var response = await _mediator.Send(query);

            return this.GetResponse(200, response);
        }

        [HttpGet("list")]
        [SwaggerOperation(
                Summary = "Получить список каналов с учетом фильтров",
                Description = "Получить список каналов с учетом фильтров",
                OperationId = "Channel.List.Get",
                Tags = new[] { "Channel" }
            )]
        public async Task<IActionResult> GetChannelList([FromQuery] GetChannelListQuery query)
        {
            var response = await _mediator.Send(query);

            return this.GetResponse(200, response);
        }

        [HttpPut("list/update-news")]
        [SwaggerOperation(
                Summary = "Обновить новостную ленту каналов",
                Description = "Обновить новостную ленту каналов",
                OperationId = "Channel.Put.Update.News",
                Tags = new[] { "Channel" }
            )]
        public async Task<IActionResult> UpdateChannel()
        {
            var command = new UpdateChannelsNewsCommand();

            await _mediator.Send(command);

            return NoContent();
        }
    }
}

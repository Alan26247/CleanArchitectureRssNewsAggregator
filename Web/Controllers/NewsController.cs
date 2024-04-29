using Core.Queries.NewsQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Web.Common.Extensions;

namespace Web.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;


        [HttpGet("list")]
        [SwaggerOperation(
                Summary = "Получить список новостей с учетом фильтров",
                Description = "Получить список Новостей с учетом фильтров",
                OperationId = "News.List.Get",
                Tags = new[] { "News" }
            )]
        public async Task<IActionResult> GetNewsList([FromQuery] GetNewsListQuery query)
        {
            var response = await _mediator.Send(query);

            return this.GetResponse(200, response);
        }
    }
}

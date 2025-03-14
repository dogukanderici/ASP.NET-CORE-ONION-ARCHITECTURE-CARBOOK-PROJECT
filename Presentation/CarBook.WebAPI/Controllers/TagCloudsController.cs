using CarBook.Application.Features.Mediator.Commands.TagCloudCommands;
using CarBook.Application.Features.Mediator.Queries.TagCloudQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagCloudsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagCloudsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> TagCloudList()
        {
            var values = await _mediator.Send(new GetTagCloudQuery());

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagCloud(Guid id)
        {
            var value = await _mediator.Send(new GetTagCloudByIdQuery(id));

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTagCloud(CreateTagCloudCommand createTagCloudCommand)
        {
            await _mediator.Send(createTagCloudCommand);

            return Ok("Blog Etiket Bilgisi Başarıyla Eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTagCloud(UpdateTagCloudCommand updateTagCloudCommand)
        {
            await _mediator.Send(updateTagCloudCommand);

            return Ok("Blog Etiket Bilgisi Başarıyla Güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveTagCloud(Guid id)
        {
            await _mediator.Send(new RemoveTagCloudCommand(id));

            return Ok("Blog Etiket Bilgisi Başarıyla Silindi.");
        }
    }
}

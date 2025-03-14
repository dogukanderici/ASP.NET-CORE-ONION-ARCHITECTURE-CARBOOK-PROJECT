using CarBook.Application.Features.Mediator.Commands.BlogTagCloudCommands;
using CarBook.Application.Features.Mediator.Queries.BlogTagCloudQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogTagCloudsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogTagCloudsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> BlogTagCloudList()
        {
            var values = await _mediator.Send(new GetBlogTagCloudQuery());

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogTagCloud(Guid id)
        {
            var value = await _mediator.Send(new GetBlogTagCloudByIdQuery(id));

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogTagCloud(CreateBlogTagCloudCommand createBlogTagCloudCommand)
        {
            await _mediator.Send(createBlogTagCloudCommand);

            return Ok("Etiket Bilgisi Blog Yazısına Başarıyla Eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlogTagCloud(UpdateBlogTagCloudCommand updateBlogTagCloudCommand)
        {
            await _mediator.Send(updateBlogTagCloudCommand);

            return Ok("Etiket Bilgisi Blog Yazısında Başarıyla Güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveBlogTagCloud(Guid id)
        {
            await _mediator.Send(new RemoveBlogTagCloudCommand(id));

            return Ok("Etiket Bilgisi Blog Yazısından Başarıyla Silindi.");
        }
    }
}

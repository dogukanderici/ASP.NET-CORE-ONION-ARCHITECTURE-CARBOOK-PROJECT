using CarBook.Application.Features.Mediator.Commands.BlogCommands;
using CarBook.Application.Features.Mediator.Queries.BlogQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> BlogList()
        {
            var values = await _mediator.Send(new GetBlogQuery());

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog(Guid id)
        {
            var value = await _mediator.Send(new GetBlogByIdQuery(id));

            return Ok(value);
        }

        [HttpGet("GetLast3Blogs")]
        public async Task<IActionResult> GetLast3Blogs()
        {
            var value = await _mediator.Send(new GetLast3BlogQuery());

            return Ok(value);
        }

        [HttpGet("GetBlogWithComment")]
        public async Task<IActionResult> GetBlogWithComment(Guid id)
        {
            var value = await _mediator.Send(new GetblogWithCommentQuery(id));

            return Ok(value);
        }

        [HttpGet("GetBlogWithPublishState")]
        public async Task<IActionResult> GetBlogWithPublishState(bool publishState, int pageDataSize, int pageNumber)
        {
            var value = await _mediator.Send(new GetBlogWithPublishStateQuery(publishState, pageDataSize, pageNumber));

            return Ok(value);
        }

        [HttpGet("GetBlogTotalCount")]
        public async Task<IActionResult> GetBlogTotalCount(bool publishState)
        {
            var totalBlogCount = await _mediator.Send(new GetBlogWithCountQuery(publishState));

            return Ok(totalBlogCount.TotalBlogCount);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogCommand createBlogCommand)
        {
            await _mediator.Send(createBlogCommand);

            return Ok("Blog Bilgisi Başarıyla Eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlog(UpdateBlogCommand updateBlogCommand)
        {
            await _mediator.Send(updateBlogCommand);

            return Ok("Blog Bilgisi Başarıyla Güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveBlog(Guid id)
        {
            await _mediator.Send(new RemoveBlogCommand(id));

            return Ok("Blog Bilgisi Başarıyla Silindi.");
        }
    }
}

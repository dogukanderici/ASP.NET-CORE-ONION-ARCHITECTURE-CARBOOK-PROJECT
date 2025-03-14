using CarBook.Application.Features.Mediator.Commands.BlogCommentCommands;
using CarBook.Application.Features.Mediator.Queries.BlogCommentQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogCommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> BlogCommentList()
        {
            var values = await _mediator.Send(new GetBlogCommentQuery());

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogComment(Guid id)
        {
            var value = await _mediator.Send(new GetBlogCommentByIdQuery(id));

            return Ok(value);
        }

        [HttpGet("GetBlogCommentByBlogId")]
        public async Task<IActionResult> GetBlogCommentByBlogId(Guid id)
        {
            var value = await _mediator.Send(new GetBlogCommentByBlogIdQuery(id));

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogComment(CreateBlogCommentCommand createBlogCommentCommand)
        {
            await _mediator.Send(createBlogCommentCommand);

            return Ok("Blog Yorum Bilgisi Başarıyla Eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlogComment(UpdateBlogCommentCommand updateBlogCommentCommand)
        {
            await _mediator.Send(updateBlogCommentCommand);

            return Ok("Blog Yorum Bilgisi Başarıyla Güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveBlogComment(Guid id)
        {
            await _mediator.Send(new RemoveBlogCommentCommand(id));

            return Ok("Blog Yorum Bilgisi Başarıyla Silindi.");
        }
    }
}

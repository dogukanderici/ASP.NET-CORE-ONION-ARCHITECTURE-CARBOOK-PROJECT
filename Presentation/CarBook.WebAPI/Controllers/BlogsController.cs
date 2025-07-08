using CarBook.Application.Features.Mediator.Commands.BlogCommands;
using CarBook.Application.Features.Mediator.Queries.BlogQueries;
using CarBook.WebAPI.Utilities.Helper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateBlogCommand> _createValidator;
        private readonly IValidator<UpdateBlogCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validatonMessageHelper;

        public BlogsController(IMediator mediator, IValidator<CreateBlogCommand> createValidator, IValidator<UpdateBlogCommand> updateValidator, IValidationResultMessageHelper validatonMessageHelper)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validatonMessageHelper = validatonMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> BlogList()
        {
            try
            {
                var values = await _mediator.Send(new GetBlogQuery());

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Blog datas reading!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetBlog(Guid id)
        {
            try
            {
                var value = await _mediator.Send(new GetBlogByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Blog data reading!");
            }
        }

        [HttpGet("GetLast3Blogs")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetLast3Blogs()
        {
            try
            {
                var value = await _mediator.Send(new GetLast3BlogQuery());

                return Ok(value);

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Last 3 Blog data reading!");
            }
        }

        [HttpGet("GetBlogWithComment")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetBlogWithComment(Guid id)
        {
            try
            {
                var value = await _mediator.Send(new GetblogWithCommentQuery(id));

                return Ok(value);

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Blog data with comments reading!");
            }
        }

        [HttpGet("GetBlogWithPublishState")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetBlogWithPublishState(bool publishState, int pageDataSize, int pageNumber)
        {
            try
            {
                var value = await _mediator.Send(new GetBlogWithPublishStateQuery(publishState, pageDataSize, pageNumber));

                return Ok(value);

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Blog data with publish state property reading!");
            }
        }

        [HttpGet("GetBlogTotalCount")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetBlogTotalCount(bool publishState)
        {
            try
            {
                var totalBlogCount = await _mediator.Send(new GetBlogWithCountQuery(publishState));

                return Ok(totalBlogCount.TotalBlogCount);

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Total Blog Count data reading!");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateBlog(CreateBlogCommand createBlogCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createBlogCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(createBlogCommand);

                    return Ok("Blog Bilgisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validatonMessageHelper.ValidationMessages(validator));

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new Blog data!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateBlog(UpdateBlogCommand updateBlogCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updateBlogCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(updateBlogCommand);

                    return Ok("Blog Bilgisi Başarıyla Güncellendi.");

                }

                return StatusCode(400, _validatonMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating Blog data!");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> RemoveBlog(Guid id)
        {
            try
            {
                await _mediator.Send(new RemoveBlogCommand(id));

                return Ok("Blog Bilgisi Başarıyla Silindi.");

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while removing Blog data!");
            }
        }
    }
}

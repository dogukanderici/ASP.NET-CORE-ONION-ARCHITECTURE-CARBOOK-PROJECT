using CarBook.Application.Features.Mediator.Commands.BlogCommentCommands;
using CarBook.Application.Features.Mediator.Queries.BlogCommentQueries;
using CarBook.Application.Validators.BlogCommentValidators;
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
    public class BlogCommentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateBlogCommentCommand> _createValidator;
        private readonly IValidator<UpdateBlogCommentCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public BlogCommentsController(IMediator mediator, IValidator<CreateBlogCommentCommand> createValidator, IValidator<UpdateBlogCommentCommand> updateValidator, IValidationResultMessageHelper validationResultMessageHelper)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }


        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> BlogCommentList()
        {
            try
            {
                var values = await _mediator.Send(new GetBlogCommentQuery());

                return Ok(values);

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Blog Comment datas reading!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetBlogComment(Guid id)
        {
            try
            {
                var value = await _mediator.Send(new GetBlogCommentByIdQuery(id));

                return Ok(value);

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Blog Comment data reading!");
            }
        }

        [HttpGet("GetBlogCommentByBlogId")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetBlogCommentByBlogId(Guid id)
        {
            try
            {
                var value = await _mediator.Send(new GetBlogCommentByBlogIdQuery(id));

                return Ok(value);

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Blog Comment for selected Blog data reading!");
            }
        }

        [HttpPost]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> CreateBlogComment(CreateBlogCommentCommand createBlogCommentCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createBlogCommentCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(createBlogCommentCommand);

                    return Ok("Blog Yorum Bilgisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new Blog Comment data!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> UpdateBlogComment(UpdateBlogCommentCommand updateBlogCommentCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updateBlogCommentCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(updateBlogCommentCommand);

                    return Ok("Blog Yorum Bilgisi Başarıyla Güncellendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating Blog Comment data!");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "FullPermissionPolicy")]
        public async Task<IActionResult> RemoveBlogComment(Guid id)
        {
            try
            {
                await _mediator.Send(new RemoveBlogCommentCommand(id));

                return Ok("Blog Yorum Bilgisi Başarıyla Silindi.");

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while deleting Blog Comment data!");
            }
        }
    }
}

using CarBook.Application.Features.Mediator.Commands.BlogTagCloudCommands;
using CarBook.Application.Features.Mediator.Queries.BlogTagCloudQueries;
using CarBook.WebAPI.Utilities.Helper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogTagCloudsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateBlogTagCloudCommand> _createValidator;
        private readonly IValidator<UpdateBlogTagCloudCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public BlogTagCloudsController(IMediator mediator, IValidator<CreateBlogTagCloudCommand> createValidator, IValidator<UpdateBlogTagCloudCommand> updateValidator, IValidationResultMessageHelper validationResultMessageHelper)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> BlogTagCloudList()
        {
            try
            {
                var values = await _mediator.Send(new GetBlogTagCloudQuery());

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading Blog Tag Cloud datas!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetBlogTagCloud(Guid id)
        {
            try
            {
                var value = await _mediator.Send(new GetBlogTagCloudByIdQuery(id));

                return Ok(value);

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading Blog Tag Cloud data!");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateBlogTagCloud(CreateBlogTagCloudCommand createBlogTagCloudCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createBlogTagCloudCommand);
                if (validator.IsValid)
                {
                    await _mediator.Send(createBlogTagCloudCommand);

                    return Ok("Etiket Bilgisi Blog Yazısına Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while adding a new Blog Tag Cloud data!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateBlogTagCloud(UpdateBlogTagCloudCommand updateBlogTagCloudCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updateBlogTagCloudCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(updateBlogTagCloudCommand);

                    return Ok("Etiket Bilgisi Blog Yazısında Başarıyla Güncellendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating Blog Tag Cloud data!");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> RemoveBlogTagCloud(Guid id)
        {
            try
            {
                await _mediator.Send(new RemoveBlogTagCloudCommand(id));

                return Ok("Etiket Bilgisi Blog Yazısından Başarıyla Silindi.");

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while deleting Blog Tag Cloud data!");
            }
        }
    }
}

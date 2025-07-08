using CarBook.Application.Features.Mediator.Commands.SocialMediaCommands;
using CarBook.Application.Features.Mediator.Commands.TagCloudCommands;
using CarBook.Application.Features.Mediator.Queries.TagCloudQueries;
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
    public class TagCloudsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateTagCloudCommand> _createValidator;
        private readonly IValidator<UpdateTagCloudCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public TagCloudsController(IMediator mediator, IValidator<CreateTagCloudCommand> createValidator, IValidator<UpdateTagCloudCommand> updateValidator, IValidationResultMessageHelper validationResultMessageHelper)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> TagCloudList()
        {
            try
            {
                var values = await _mediator.Send(new GetTagCloudQuery());

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading all tag cloud datas!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetTagCloud(Guid id)
        {
            try
            {
                var value = await _mediator.Send(new GetTagCloudByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading tag cloud data");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateTagCloud(CreateTagCloudCommand createTagCloudCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createTagCloudCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(createTagCloudCommand);

                    return Ok("Blog Etiket Bilgisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new tag cloud data!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateTagCloud(UpdateTagCloudCommand updateTagCloudCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updateTagCloudCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(updateTagCloudCommand);

                    return Ok("Blog Etiket Bilgisi Başarıyla Güncellendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating tag cloud data!");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> RemoveTagCloud(Guid id)
        {
            try
            {
                await _mediator.Send(new RemoveTagCloudCommand(id));

                return Ok("Blog Etiket Bilgisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured deleting tag cloud data!");
            }
        }
    }
}

using CarBook.Application.Features.Mediator.Commands.SocialMediaCommands;
using CarBook.Application.Features.Mediator.Queries.SocialMediaQueries;
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
    public class SocialMediasController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateSocialMediaCommand> _createValidator;
        private readonly IValidator<UpdateSocialMediaCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public SocialMediasController(IMediator mediator, IValidator<CreateSocialMediaCommand> createValidator, IValidator<UpdateSocialMediaCommand> updateValidator, IValidationResultMessageHelper validationResultMessageHelper)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> SocialMediaList()
        {
            try
            {
                var values = await _mediator.Send(new GetSocialMediaQuery());

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading all social media datas!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetSocialMedia(int id)
        {
            try
            {
                var value = await _mediator.Send(new GetSocialMediaByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading social media data!");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaCommand createSocialMediaCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createSocialMediaCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(createSocialMediaCommand);

                    return Ok("Sosyal Medya Bilgisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new social media data!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaCommand updateSocialMediaCommand)
        {

            try
            {
                ValidationResult validator = _updateValidator.Validate(updateSocialMediaCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(updateSocialMediaCommand);

                    return Ok("Sosyal Medya Bilgisi Başarıyla Güncellendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating social media data!");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> RemoveSocialMedia(int id)
        {
            try
            {
                await _mediator.Send(new RemoveSocialMediaCommand(id));

                return Ok("Sosyal Medya Bilgisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while deleting social media data!");
            }
        }
    }
}

using CarBook.Application.Features.CQRS.Commands.AboutCommands;
using CarBook.Application.Features.CQRS.Handlers.AboutHandlers;
using CarBook.Application.Features.CQRS.Queries.AboutQueries;
using CarBook.Application.Validators.AboutValidators;
using CarBook.WebAPI.Utilities.Helper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : BaseController
    {
        private readonly GetAboutQueryHandler _getAboutQueryHandler;
        private readonly GetAboutByIdQueryHandler _getAboutByIdQueryHandler;
        private readonly CreateAboutCommandHandler _createAboutCommandHandler;
        private readonly UpdateAboutCommandHandler _updateAboutCommandHandler;
        private readonly RemoveAboutCommandHandler _removeAboutCommandHandler;
        private readonly IValidator<CreateAboutCommand> _createValidator;
        private readonly IValidator<UpdateAboutCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public AboutsController(GetAboutQueryHandler getAboutQueryHandler,
            GetAboutByIdQueryHandler getAboutByIdQueryHandler,
            CreateAboutCommandHandler createAboutCommandHandler,
            UpdateAboutCommandHandler updateAboutCommandHandler,
            RemoveAboutCommandHandler removeAboutCommandHandler,
            IValidator<CreateAboutCommand> createValidator,
            IValidator<UpdateAboutCommand> updateValidator,
            IValidationResultMessageHelper validationResultMessageHelper)
        {
            _getAboutQueryHandler = getAboutQueryHandler;
            _getAboutByIdQueryHandler = getAboutByIdQueryHandler;
            _createAboutCommandHandler = createAboutCommandHandler;
            _updateAboutCommandHandler = updateAboutCommandHandler;
            _removeAboutCommandHandler = removeAboutCommandHandler;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> AboutList()
        {
            try
            {
                var values = await _getAboutQueryHandler.Handle();

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An Error occured while About datas reading.");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> AboutList(int id)
        {
            try
            {
                var values = await _getAboutByIdQueryHandler.Handle(new GetAboutByIdQuery(id));

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An Error occured while a new About data reading.");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateAbout(CreateAboutCommand createAboutCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createAboutCommand);

                if (validator.IsValid)
                {
                    await _createAboutCommandHandler.Handle(createAboutCommand);

                    return Ok("Hakkımızda Verisi Başarıyla Eklendi.");
                }

                Dictionary<string, string[]> validationErrors = _validationResultMessageHelper.ValidationMessages(validator);

                return StatusCode(400, validationErrors);
            }
            catch (Exception ex)
            {
                return BadRequest("An Error occured while a new About data adding");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateAbout(UpdateAboutCommand updateAboutCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updateAboutCommand);

                if (validator.IsValid)
                {
                    await _updateAboutCommandHandler.Handle(updateAboutCommand);

                    return Ok("Hakkımızda Verisi Başarıyla Güncellendi.");
                }
                else
                {
                    Dictionary<string, string[]> validationResult = _validationResultMessageHelper.ValidationMessages(validator);

                    return StatusCode(400, validationResult);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An Error occured while About data updating.");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            try
            {
                await _removeAboutCommandHandler.Handle(new RemoveAboutCommand(id));

                return Ok("Hakkımızda Verisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An Error occured while About data deleting.");
            }
        }
    }
}

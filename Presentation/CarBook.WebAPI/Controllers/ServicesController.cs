using CarBook.Application.Features.Mediator.Commands.ServiceCommands;
using CarBook.Application.Features.Mediator.Queries.ServiceQueries;
using CarBook.Application.Validators.ServiceValidators;
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
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateServiceCommand> _createValidator;
        private readonly IValidator<UpdateServiceCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public ServicesController(IMediator mediator, IValidator<CreateServiceCommand> createValidator, IValidator<UpdateServiceCommand> updateValidator, IValidationResultMessageHelper validationResultMessageHelper)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> ServiceList()
        {
            try
            {
                var values = await _mediator.Send(new GetServiceQuery());

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading all service datas!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetService(int id)
        {
            try
            {
                var value = await _mediator.Send(new GetServiceByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading service data!");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateService(CreateServiceCommand createServiceCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createServiceCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(createServiceCommand);

                    return Ok("Hizmet Bilgisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new service data!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateService(UpdateServiceCommand updateServiceCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updateServiceCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(updateServiceCommand);

                    return Ok("Hizmet Bilgisi Başarıyla Güncellendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating service data!");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> RemoveService(int id)
        {
            try
            {
                await _mediator.Send(new RemoveServiceCommand(id));

                return Ok("Hizmet Bilgisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while deleting service data!");
            }
        }
    }
}

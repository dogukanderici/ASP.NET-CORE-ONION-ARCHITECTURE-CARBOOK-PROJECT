using CarBook.Application.Features.Mediator.Commands.LocationCommands;
using CarBook.Application.Features.Mediator.Queries.LocationQueries;
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
    public class LocationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateLocationCommand> _createValidator;
        private readonly IValidator<UpdateLocationCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public LocationsController(IMediator mediator, IValidator<CreateLocationCommand> createValidator, IValidator<UpdateLocationCommand> updateValidator, IValidationResultMessageHelper validationResultMessageHelper)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> LocationList()
        {
            try
            {
                var values = await _mediator.Send(new GetLocationQuery());

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading all location list data!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetLocation(int id)
        {
            try
            {
                var value = await _mediator.Send(new GetLocationByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading location data!");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateLocation(CreateLocationCommand createLocationCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createLocationCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(createLocationCommand);

                    return Ok("Lokasyon Bilgisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new location data!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateLocation(UpdateLocationCommand updateLocationCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updateLocationCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(updateLocationCommand);

                    return Ok("Lokasyon Bilgisi Başarıyla Güncellendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating location data!");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> RemoveLocation(int id)
        {
            try
            {
                await _mediator.Send(new RemoveLocationCommand(id));

                return Ok("Lokasyon Bilgisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while deleting location data!");
            }
        }
    }
}

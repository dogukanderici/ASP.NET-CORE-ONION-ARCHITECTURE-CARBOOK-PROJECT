using CarBook.Application.Features.Mediator.Commands.ReservationCommands;
using CarBook.Application.Features.Mediator.Queries.ReservationQueries;
using CarBook.WebAPI.Utilities.Helper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateReservationCommand> _createValidator;
        private readonly IValidator<UpdateReservationCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public ReservationsController(IMediator mediator, IValidator<CreateReservationCommand> createValidator, IValidator<UpdateReservationCommand> updateValidator, IValidationResultMessageHelper validationResultMessageHelper)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        public async Task<IActionResult> ReservationList()
        {
            try
            {
                var values = await _mediator.Send(new GetReservationQuery());

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading all reservation datas!");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservation(Guid id)
        {
            try
            {
                var value = await _mediator.Send(new GetReservationByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading reservation data!");
            }
        }

        [HttpGet("ReservationByEmail/{email}")]
        public async Task<IActionResult> GetReservationByEmail(string email)
        {

            try
            {
                var value = await _mediator.Send(new GetReservationByEmailQuery(email));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading reservation data with e-mail!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationCommand createReservationCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createReservationCommand);

                if (validator.IsValid)
                {
                    var responseValue = await _mediator.Send(createReservationCommand);

                    if (responseValue.ResponseState)
                    {
                        return Ok("Rezervasyon Bilgisi Başarıyla Eklendi.");
                    }
                    else
                    {
                        return BadRequest(responseValue.ResponseMessage);
                    }
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new reservation data!");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservation(UpdateReservationCommand updateReservationCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updateReservationCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(updateReservationCommand);

                    return Ok("Rezervasyon Bilgisi Başarıyla Güncellendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating reservation data!");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveReservation(Guid id)
        {
            try
            {
                await _mediator.Send(new RemoveReservationCommand(id));

                return Ok("Rezervasyon Bilgisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while deleting reservation data!");
            }
        }
    }
}

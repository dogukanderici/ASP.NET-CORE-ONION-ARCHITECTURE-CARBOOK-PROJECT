using CarBook.Application.Features.Mediator.Commands.ReservationCommands;
using CarBook.Application.Features.Mediator.Queries.ReservationQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ReservationList()
        {
            var values = await _mediator.Send(new GetReservationQuery());

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservation(Guid id)
        {
            var value = await _mediator.Send(new GetReservationByIdQuery(id));

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationCommand createReservationCommand)
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

        [HttpPut]
        public async Task<IActionResult> UpdateReservation(UpdateReservationCommand updateReservationCommand)
        {
            await _mediator.Send(updateReservationCommand);

            return Ok("Rezervasyon Bilgisi Başarıyla Güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveReservation(Guid id)
        {
            await _mediator.Send(new RemoveReservationCommand(id));

            return Ok("Rezervasyon Bilgisi Başarıyla Silindi.");
        }
    }
}

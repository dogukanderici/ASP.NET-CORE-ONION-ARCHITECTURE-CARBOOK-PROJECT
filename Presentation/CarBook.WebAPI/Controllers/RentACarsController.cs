using CarBook.Application.Features.Mediator.Commands.RentACarCommands;
using CarBook.Application.Features.Mediator.Queries.RentACarQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentACarsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentACarsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> RentACarList()
        {
            var values = await _mediator.Send(new GetRentACarQuery());

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentACar(Guid id)
        {
            var value = await _mediator.Send(new GetRentACarByIdQuery(id));

            return Ok(value);
        }

        [HttpGet("GetRentACarWithAvailablity")]
        public async Task<IActionResult> GetRentACarWithAvailablity(int id, bool state, DateTimeOffset pickUpDate, DateTimeOffset dropOffDate)
        {
            var value = await _mediator.Send(new GetRentACarByAvailablityQuery(id, state, pickUpDate, dropOffDate));

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRentACar(CreateRentACarCommand createRentACarCommand)
        {
            await _mediator.Send(createRentACarCommand);

            return Ok("Araç Durum Bilgisi Başarıyla Eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRentACar(UpdateRentACarCommand updateRentACarCommand)
        {
            await _mediator.Send(updateRentACarCommand);

            return Ok("Araç Durum Bilgisi Başarıyla Güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRentACar(Guid id)
        {
            await _mediator.Send(new RemoveRentACarCommand(id));

            return Ok("Araç Durum Bilgisi Başarıyla Silindi.");
        }
    }
}

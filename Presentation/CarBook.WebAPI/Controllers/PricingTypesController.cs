using CarBook.Application.Features.Mediator.Commands.PricingTypeCommands;
using CarBook.Application.Features.Mediator.Queries.PricingTypeQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PricingTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> PricingTypeList()
        {
            var values = await _mediator.Send(new GetPricingTypeQuery());

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPricingType(int id)
        {
            var value = await _mediator.Send(new GetPricingTypeByIdQuery(id));

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePricingType(CreatePricingTypeCommand createPricingTypeCommand)
        {
            await _mediator.Send(createPricingTypeCommand);

            return Ok("Ödeme Planı Bilgisi Başarıyla Eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePricingType(UpdatePricingTypeCommand updatePricingTypeCommand)
        {
            await _mediator.Send(updatePricingTypeCommand);

            return Ok("Ödeme Planı Bilgisi Başarıyla Güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemovePricingType(int id)
        {
            await _mediator.Send(new RemovePricingTypeCommand(id));

            return Ok("Ödeme Planı Bilgisi Başarıyla Silindi.");
        }
    }
}

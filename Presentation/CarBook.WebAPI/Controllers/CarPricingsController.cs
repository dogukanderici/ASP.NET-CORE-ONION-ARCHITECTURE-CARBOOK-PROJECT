using CarBook.Application.Features.CQRS.Commands.CarPricingCommands;
using CarBook.Application.Features.CQRS.Handlers.CarPricingHandlers;
using CarBook.Application.Features.CQRS.Queries.CarPricingQueries;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarPricingsController : ControllerBase
    {
        private readonly GetCarPricingQueryHandler _getCarPricingQueryHandler;
        private readonly GetCarPricingByIdQueryHandler _getCarPricingByIdQueryHandler;
        private readonly GetCarPricingByCarIdQueryHandler _getCarPricingByCarIdQueryHandler;
        private readonly CreateCarPricingCommandHandler _createCarPricingCommandHandler;
        private readonly UpdateCarPricingCommandHandler _updateCarPricingCommandHandler;
        private readonly RemoveCarPricingCommandHandler _removeCarPricingCommandHandler;

        public CarPricingsController(GetCarPricingQueryHandler getCarPricingQueryHandler,
            GetCarPricingByIdQueryHandler getCarPricingByIdQueryHandler,
            GetCarPricingByCarIdQueryHandler getCarPricingByCarIdQueryHandler,
            CreateCarPricingCommandHandler createCarPricingCommandHandler,
            UpdateCarPricingCommandHandler updateCarPricingCommandHandler,
            RemoveCarPricingCommandHandler removeCarPricingCommandHandler)
        {
            _getCarPricingQueryHandler = getCarPricingQueryHandler;
            _getCarPricingByIdQueryHandler = getCarPricingByIdQueryHandler;
            _getCarPricingByCarIdQueryHandler = getCarPricingByCarIdQueryHandler;
            _createCarPricingCommandHandler = createCarPricingCommandHandler;
            _updateCarPricingCommandHandler = updateCarPricingCommandHandler;
            _removeCarPricingCommandHandler = removeCarPricingCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> CarPricingList()
        {
            var values = await _getCarPricingQueryHandler.Handle();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CarPricingList(int id)
        {
            var values = await _getCarPricingByIdQueryHandler.Handle(new GetCarPricingByIdQuery(id));
            return Ok(values);
        }

        [HttpGet("GetCarPricingByCarId")]
        public async Task<IActionResult> GetCarPricingByCarId(int id)
        {
            var values = await _getCarPricingByCarIdQueryHandler.Handle(new GetCarPricingByCarIdQuery(id));
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarPricing(CreateCarPricingCommand createCarPricingCommand)
        {
            await _createCarPricingCommandHandler.Handle(createCarPricingCommand);
            return Ok("Araç Ödeme Bilgisi Verisi Başarıyla Eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCarPricing(UpdateCarPricingCommand updateCarPricingCommand)
        {
            await _updateCarPricingCommandHandler.Handle(updateCarPricingCommand);

            return Ok("Araç Ödeme Bilgisi Verisi Başarıyla Güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCarPricing(int id)
        {
            await _removeCarPricingCommandHandler.Handle(new RemoveCarPricingCommand(id));

            return Ok("Araç Ödeme Bilgisi Verisi Başarıyla Silindi.");
        }
    }
}

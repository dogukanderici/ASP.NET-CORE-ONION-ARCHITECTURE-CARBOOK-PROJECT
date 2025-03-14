using CarBook.Application.Features.CQRS.Commands.CarFeatureCommands;
using CarBook.Application.Features.CQRS.Handlers.CarFeatureHandlers;
using CarBook.Application.Features.CQRS.Queries.CarFeatureQueries;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarFeaturesController : ControllerBase
    {
        private readonly GetCarFeatureQueryHandler _getCarFeatureQueryHandler;
        private readonly GetCarFeatureByIdQueryHandler _getCarFeatureByIdQueryHandler;
        private readonly CreateCarFeatureCommandHandler _createCarFeatureCommandHandler;
        private readonly UpdateCarFeatureCommandHandler _updateCarFeatureCommandHandler;
        private readonly RemoveCarFeatureCommandHandler _removeCarFeatureCommandHandler;

        public CarFeaturesController(GetCarFeatureQueryHandler getCarFeatureQueryHandler,
            GetCarFeatureByIdQueryHandler getCarFeatureByIdQueryHandler,
            CreateCarFeatureCommandHandler createCarFeatureCommandHandler,
            UpdateCarFeatureCommandHandler updateCarFeatureCommandHandler,
            RemoveCarFeatureCommandHandler removeCarFeatureCommandHandler)
        {
            _getCarFeatureQueryHandler = getCarFeatureQueryHandler;
            _getCarFeatureByIdQueryHandler = getCarFeatureByIdQueryHandler;
            _createCarFeatureCommandHandler = createCarFeatureCommandHandler;
            _updateCarFeatureCommandHandler = updateCarFeatureCommandHandler;
            _removeCarFeatureCommandHandler = removeCarFeatureCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> CarFeatureList()
        {
            var values = await _getCarFeatureQueryHandler.Handle();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CarFeatureList(int id)
        {
            var values = await _getCarFeatureByIdQueryHandler.Handle(new GetCarFeatureByIdQuery(id));
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarFeature(CreateCarFeatureCommand createCarFeatureCommand)
        {
            await _createCarFeatureCommandHandler.Handle(createCarFeatureCommand);
            return Ok("Araca Ait Özellik Verisi Başarıyla Eklendi.");
        }

        [HttpPost("CreateCarFeatureWithList")]
        public async Task<IActionResult> CreateCarFeatureWithList(List<CreateCarFeatureCommand> createCarFeatureCommand)
        {
            foreach (var item in createCarFeatureCommand)
            {
                await _createCarFeatureCommandHandler.Handle(item);
            }

            return Ok("Araca Ait Özellik Verisi Başarıyla Eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCarFeature(UpdateCarFeatureCommand updateCarFeatureCommand)
        {
            await _updateCarFeatureCommandHandler.Handle(updateCarFeatureCommand);

            return Ok("Araca Ait Özellik Verisi Başarıyla Güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCarFeature(int id)
        {
            await _removeCarFeatureCommandHandler.Handle(new RemoveCarFeatureCommand(id));

            return Ok("Araca Ait Özellik Verisi Başarıyla Silindi.");
        }
    }
}

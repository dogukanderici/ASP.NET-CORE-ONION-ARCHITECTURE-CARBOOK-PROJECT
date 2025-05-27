using CarBook.Application.Features.CQRS.Commands.CarReviewCommands;
using CarBook.Application.Features.CQRS.Handlers.CarReviewHandlers;
using CarBook.Application.Features.CQRS.Queries.CarReviewQueries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarReviewsController : ControllerBase
    {
        private readonly GetCarReviewQueryHandler _getCarReviewQueryHandler;
        private readonly GetCarReviewByIdQueryHandler _getCarReviewByIdQueryHandler;
        private readonly GetCarReviewByCarIdQueryHandler _getCarReviewByCarIdQueryHandler;
        private readonly CreateCarReviewCommandHandler _createCarReviewCommandHandler;
        private readonly UpdateCarReviewCommandHandler _updateCarReviewCommandHandler;
        private readonly RemoveCarReviewCommandHandler _removeCarReviewCommandHandler;

        public CarReviewsController(GetCarReviewQueryHandler getCarReviewQueryHandler,
            GetCarReviewByIdQueryHandler getCarReviewByIdQueryHandler,
            GetCarReviewByCarIdQueryHandler getCarReviewByCarIdQueryHandler,
            CreateCarReviewCommandHandler createCarReviewCommandHandler,
            UpdateCarReviewCommandHandler updateCarReviewCommandHandler,
            RemoveCarReviewCommandHandler removeCarReviewCommandHandler)
        {
            _getCarReviewQueryHandler = getCarReviewQueryHandler;
            _getCarReviewByIdQueryHandler = getCarReviewByIdQueryHandler;
            _getCarReviewByCarIdQueryHandler = getCarReviewByCarIdQueryHandler;
            _createCarReviewCommandHandler = createCarReviewCommandHandler;
            _updateCarReviewCommandHandler = updateCarReviewCommandHandler;
            _removeCarReviewCommandHandler = removeCarReviewCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarReviewList()
        {
            try
            {
                var values = await _getCarReviewQueryHandler.Handle();

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest($"Araçlara Ait Değerlendirmeler Listelenirken Bir Hata Oluştu! Error Message: {ex}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarReviewById(Guid id)
        {
            try
            {
                var value = await _getCarReviewByIdQueryHandler.Handle(new GetCarReviewByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest($"{id} Araç Değerlendirmesi Listelenirken Bir Hata Oluştu! Error Message: {ex}");
            }
        }

        [HttpGet("CarReviewWithCar/{id}")]
        public async Task<IActionResult> GetCarReviewWithCarID(int id)
        {
            try
            {
                var value = await _getCarReviewByCarIdQueryHandler.Handle(new GetCarReviewByCarIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest($"{id} Araca Ait Araç Değerlendirmeleri Listelenirken Bir Hata Oluştu! Error Message: {ex}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarReview(CreateCarReviewCommand createCarReviewCommand)
        {
            try
            {
                await _createCarReviewCommandHandler.Handle(createCarReviewCommand);

                return Ok($"{createCarReviewCommand.CarID} Araca Ait Yeni Değerlendirme Başarıyla Eklendi!");
            }
            catch (Exception ex)
            {
                return BadRequest($"{createCarReviewCommand.CarID} Araca Ait Yeni Değerlendirme Eklenirken Bir Hata Oluştu! Error Message: {ex}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCarReview(UpdateCarReviewCommand updateCarReviewCommand)
        {
            try
            {
                await _updateCarReviewCommandHandler.Handle(updateCarReviewCommand);

                return Ok($"{updateCarReviewCommand.CarID} Araca Ait Değerlendirme Başarıyla Güncellend!");
            }
            catch (Exception ex)
            {
                return BadRequest($"{updateCarReviewCommand.CarID} Araca Ait Değerlendirme Güncellenirken Bir Hata Oluştu! Error Message: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCarReview(Guid id)
        {
            try
            {
                await _removeCarReviewCommandHandler.Handle(new RemoveCarReviewCommand(id));

                return Ok($"{id} Değerlendirme Başarıyla Silindi!");
            }
            catch (Exception ex)
            {
                return BadRequest($"{id} Değerlendirme Silinirken Bir Hata Oluştu! Error Message: {ex}");
            }
        }
    }
}

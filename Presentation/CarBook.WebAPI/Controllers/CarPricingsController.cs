using CarBook.Application.Features.CQRS.Commands.CarPricingCommands;
using CarBook.Application.Features.CQRS.Handlers.CarPricingHandlers;
using CarBook.Application.Features.CQRS.Queries.CarPricingQueries;
using CarBook.WebAPI.Utilities.Helper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IValidator<CreateCarPricingCommand> _createValidator;
        private readonly IValidator<UpdateCarPricingCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public CarPricingsController(GetCarPricingQueryHandler getCarPricingQueryHandler,
            GetCarPricingByIdQueryHandler getCarPricingByIdQueryHandler,
            GetCarPricingByCarIdQueryHandler getCarPricingByCarIdQueryHandler,
            CreateCarPricingCommandHandler createCarPricingCommandHandler,
            UpdateCarPricingCommandHandler updateCarPricingCommandHandler,
            RemoveCarPricingCommandHandler removeCarPricingCommandHandler,
            IValidator<CreateCarPricingCommand> createValidator,
            IValidator<UpdateCarPricingCommand> updateValidator,
            IValidationResultMessageHelper validationResultMessageHelper)
        {
            _getCarPricingQueryHandler = getCarPricingQueryHandler;
            _getCarPricingByIdQueryHandler = getCarPricingByIdQueryHandler;
            _getCarPricingByCarIdQueryHandler = getCarPricingByCarIdQueryHandler;
            _createCarPricingCommandHandler = createCarPricingCommandHandler;
            _updateCarPricingCommandHandler = updateCarPricingCommandHandler;
            _removeCarPricingCommandHandler = removeCarPricingCommandHandler;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        public async Task<IActionResult> CarPricingList()
        {
            try
            {
                var values = await _getCarPricingQueryHandler.Handle();
                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Car Pricing datas reading!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> CarPricingList(int id)
        {
            try
            {
                var values = await _getCarPricingByIdQueryHandler.Handle(new GetCarPricingByIdQuery(id));
                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("AN error occured while Car Pricing data reading!");
            }
        }

        [HttpGet("GetCarPricingByCarId")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetCarPricingByCarId(int id)
        {
            try
            {
                var values = await _getCarPricingByCarIdQueryHandler.Handle(new GetCarPricingByCarIdQuery(id));
                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("AN error occured while Car Pricing data with Car Id reading!");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateCarPricing(CreateCarPricingCommand createCarPricingCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createCarPricingCommand);

                if (validator.IsValid)
                {
                    await _createCarPricingCommandHandler.Handle(createCarPricingCommand);
                    return Ok("Araç Ödeme Bilgisi Verisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new Car Pricing data!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateCarPricing(UpdateCarPricingCommand updateCarPricingCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updateCarPricingCommand);

                if (validator.IsValid)
                {
                    await _updateCarPricingCommandHandler.Handle(updateCarPricingCommand);

                    return Ok("Araç Ödeme Bilgisi Verisi Başarıyla Güncellendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating Car Pricing data!");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> DeleteCarPricing(int id)
        {
            try
            {
                await _removeCarPricingCommandHandler.Handle(new RemoveCarPricingCommand(id));

                return Ok("Araç Ödeme Bilgisi Verisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while deleting Car Pricing data!");
            }
        }
    }
}

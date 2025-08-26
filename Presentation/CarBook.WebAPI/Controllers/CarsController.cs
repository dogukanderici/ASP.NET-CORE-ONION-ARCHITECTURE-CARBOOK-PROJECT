using CarBook.Application.Features.CQRS.Commands.CarCommands;
using CarBook.Application.Features.CQRS.Handlers.CarHandlers;
using CarBook.Application.Features.CQRS.Queries.CarQueries;
using CarBook.WebAPI.Utilities.Helper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly GetCarQueryHandler _getCarQueryHandler;
        private readonly GetCarForOnlyCarPricingQueryHandler _getCarForOnlyCarPricingQueryHandler;
        private readonly GetLast5CarsQueryHandler _getLast5CarsQueryHandler;
        private readonly GetCarByIdQueryHandler _getCarByIdQueryHandler;
        private readonly CreateCarCommandHandler _createCarCommandHandler;
        private readonly UpdateCarCommandHandler _updateCarCommandHandler;
        private readonly RemoveCarCommandHandler _removeCarCommandHandler;
        private readonly IValidator<CreateCarCommand> _createValidator;
        private readonly IValidator<UpdateCarCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public CarsController(
            GetCarQueryHandler getCarQueryHandler,
            GetCarForOnlyCarPricingQueryHandler getCarForOnlyCarPricingQueryHandler,
            GetCarByIdQueryHandler getCarByIdQueryHandler,
            GetLast5CarsQueryHandler getLast5CarsQueryHandler,
            CreateCarCommandHandler createCarCommandHandler,
            UpdateCarCommandHandler updateCarCommandHandler,
            RemoveCarCommandHandler removeCarCommandHandler,
            IValidator<CreateCarCommand> createValidator,
            IValidator<UpdateCarCommand> updateValidator,
            IValidationResultMessageHelper validationResultMessageHelper)
        {
            _getCarQueryHandler = getCarQueryHandler;
            _getCarForOnlyCarPricingQueryHandler = getCarForOnlyCarPricingQueryHandler;
            _getLast5CarsQueryHandler = getLast5CarsQueryHandler;
            _getCarByIdQueryHandler = getCarByIdQueryHandler;
            _createCarCommandHandler = createCarCommandHandler;
            _updateCarCommandHandler = updateCarCommandHandler;
            _removeCarCommandHandler = removeCarCommandHandler;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet("{skipNumber?}/{takeNumber?}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> CarList(int? skipNumber, int? takeNumber)
        {
            try
            {
                var values = await _getCarQueryHandler.Handle(new GetCarQuery(skipNumber, takeNumber));

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Car datas reading!");
            }
        }

        [HttpGet("GetCarForOnlyWithPricing")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetCarForOnlyWithPricing()
        {
            try
            {
                var values = await _getCarForOnlyCarPricingQueryHandler.Handle();

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Car data with pricing reading!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetCar(int id)
        {
            try
            {
                var value = await _getCarByIdQueryHandler.Handle(new GetCarByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Car data reading!");
            }
        }

        [HttpGet("GetLast5Cars")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetLast5Cars()
        {
            try
            {
                var value = await _getLast5CarsQueryHandler.Handle();

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Last 5 Car data reading!");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateCar(CreateCarCommand createCarCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createCarCommand);

                if (validator.IsValid)
                {
                    await _createCarCommandHandler.Handle(createCarCommand);

                    return Ok("Otomail Bilgisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new Car data!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateCar(UpdateCarCommand updateCarCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updateCarCommand);

                if (validator.IsValid)
                {
                    await _updateCarCommandHandler.Handle(updateCarCommand);

                    return Ok("Otomail Bilgisi Başarıyla Güncellendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating Car data!");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> RemoveCar(int id)
        {
            try
            {
                await _removeCarCommandHandler.Handle(new RemoveCarCommand(id));

                return Ok("Otomail Bilgisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while deleting Car data!");
            }
        }
    }
}

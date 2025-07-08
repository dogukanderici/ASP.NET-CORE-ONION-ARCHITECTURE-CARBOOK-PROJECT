using CarBook.Application.Features.CQRS.Commands.CarFeatureCommands;
using CarBook.Application.Features.CQRS.Handlers.CarFeatureHandlers;
using CarBook.Application.Features.CQRS.Queries.CarFeatureQueries;
using CarBook.WebAPI.Utilities.Helper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IValidator<CreateCarFeatureCommand> _createValidator;
        private readonly IValidator<UpdateCarFeatureCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public CarFeaturesController(GetCarFeatureQueryHandler getCarFeatureQueryHandler,
            GetCarFeatureByIdQueryHandler getCarFeatureByIdQueryHandler,
            CreateCarFeatureCommandHandler createCarFeatureCommandHandler,
            UpdateCarFeatureCommandHandler updateCarFeatureCommandHandler,
            RemoveCarFeatureCommandHandler removeCarFeatureCommandHandler,
            IValidator<CreateCarFeatureCommand> createValidator,
            IValidator<UpdateCarFeatureCommand> updateValidator,
            IValidationResultMessageHelper validationResultMessageHelper)
        {
            _getCarFeatureQueryHandler = getCarFeatureQueryHandler;
            _getCarFeatureByIdQueryHandler = getCarFeatureByIdQueryHandler;
            _createCarFeatureCommandHandler = createCarFeatureCommandHandler;
            _updateCarFeatureCommandHandler = updateCarFeatureCommandHandler;
            _removeCarFeatureCommandHandler = removeCarFeatureCommandHandler;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> CarFeatureList()
        {
            try
            {
                var values = await _getCarFeatureQueryHandler.Handle();
                return Ok(values);

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading Car Feature datas!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> CarFeatureList(int id)
        {
            try
            {
                var values = await _getCarFeatureByIdQueryHandler.Handle(new GetCarFeatureByIdQuery(id));
                return Ok(values);

            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading Car Feature data!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarFeature(CreateCarFeatureCommand createCarFeatureCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createCarFeatureCommand);

                if (validator.IsValid)
                {
                    await _createCarFeatureCommandHandler.Handle(createCarFeatureCommand);
                    return Ok("Araca Ait Özellik Verisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new Car Feature data!");
            }
        }

        [HttpPost("CreateCarFeatureWithList")]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateCarFeatureWithList(List<CreateCarFeatureCommand> createCarFeatureCommand)
        {
            try
            {
                List<Dictionary<string, string[]>> validatonErrorsList = new List<Dictionary<string, string[]>>();

                foreach (var item in createCarFeatureCommand)
                {
                    ValidationResult validator = _createValidator.Validate(item);
                    if (!validator.IsValid)
                    {
                        validatonErrorsList.Add(_validationResultMessageHelper.ValidationMessages(validator));
                    }
                }

                if (validatonErrorsList.Any())
                {
                    return StatusCode(400, validatonErrorsList);
                }

                foreach (var item in createCarFeatureCommand)
                {
                    await _createCarFeatureCommandHandler.Handle(item);
                }

                return Ok("Araca Ait Özellik Verileri Başarıyla Eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while adding new Car Feature datas!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateCarFeature(UpdateCarFeatureCommand updateCarFeatureCommand)
        {
            await _updateCarFeatureCommandHandler.Handle(updateCarFeatureCommand);

            return Ok("Araca Ait Özellik Verisi Başarıyla Güncellendi.");
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> DeleteCarFeature(int id)
        {
            await _removeCarFeatureCommandHandler.Handle(new RemoveCarFeatureCommand(id));

            return Ok("Araca Ait Özellik Verisi Başarıyla Silindi.");
        }
    }
}

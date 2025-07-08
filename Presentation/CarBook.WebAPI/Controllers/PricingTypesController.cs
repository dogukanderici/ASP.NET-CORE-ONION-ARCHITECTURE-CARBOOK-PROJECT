using CarBook.Application.Features.Mediator.Commands.PricingTypeCommands;
using CarBook.Application.Features.Mediator.Queries.PricingTypeQueries;
using CarBook.WebAPI.Utilities.Helper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreatePricingTypeCommand> _createValidator;
        private readonly IValidator<UpdatePricingTypeCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public PricingTypesController(IMediator mediator, IValidator<CreatePricingTypeCommand> createValidator, IValidator<UpdatePricingTypeCommand> updateValidator, IValidationResultMessageHelper validationResultMessageHelper)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> PricingTypeList()
        {
            try
            {
                var values = await _mediator.Send(new GetPricingTypeQuery());

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading all pricing type datas!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetPricingType(int id)
        {
            try
            {
                var value = await _mediator.Send(new GetPricingTypeByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading pricing type data!");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreatePricingType(CreatePricingTypeCommand createPricingTypeCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createPricingTypeCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(createPricingTypeCommand);

                    return Ok("Ödeme Planı Bilgisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new pricing type data!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdatePricingType(UpdatePricingTypeCommand updatePricingTypeCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updatePricingTypeCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(updatePricingTypeCommand);

                    return Ok("Ödeme Planı Bilgisi Başarıyla Güncellendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating pricing type data!");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> RemovePricingType(int id)
        {
            try
            {
                await _mediator.Send(new RemovePricingTypeCommand(id));

                return Ok("Ödeme Planı Bilgisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while deleting pricing type data!");
            }
        }
    }
}

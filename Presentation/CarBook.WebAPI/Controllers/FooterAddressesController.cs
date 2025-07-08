using CarBook.Application.Features.Mediator.Commands.FooterAddressCommands;
using CarBook.Application.Features.Mediator.Queries.FooterAddressQueries;
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
    public class FooterAddressesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateFooterAddressCommand> _createValidator;
        private readonly IValidator<UpdateFooterAddressCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public FooterAddressesController(IMediator mediator, IValidator<CreateFooterAddressCommand> createValidator, IValidator<UpdateFooterAddressCommand> updateValidator, IValidationResultMessageHelper validationResultMessageHelper)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> FooterAddressList()
        {
            try
            {
                var values = await _mediator.Send(new GetFooterAddressQuery());

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading all footer address datas!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetFooterAddress(int id)
        {
            try
            {
                var value = await _mediator.Send(new GetFooterAddressByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading footer address data!");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateFooterAddress(CreateFooterAddressCommand createFooterAddressCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createFooterAddressCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(createFooterAddressCommand);

                    return Ok("Adres Bilgisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new footer address data!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateFooterAddress(UpdateFooterAddressCommand updateFooterAddressCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updateFooterAddressCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(updateFooterAddressCommand);

                    return Ok("Adres Bilgisi Başarıyla Güncellendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating footer address data!");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> RemoveFooterAddress(int id)
        {
            try
            {
                await _mediator.Send(new RemoveFooterAddressCommand(id));

                return Ok("Adres Bilgisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while deleting footer address data!");
            }
        }

    }
}

using CarBook.Application.Features.Mediator.Commands.FeatureCommands;
using CarBook.Application.Features.Mediator.Queries.FeatureQueries;
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
    public class FeaturesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateFeatureCommand> _createValiadtor;
        private readonly IValidator<UpdateFeatureCommand> _updateValiadtor;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public FeaturesController(IMediator mediator, IValidator<CreateFeatureCommand> createValiadtor, IValidator<UpdateFeatureCommand> updateValiadtor, IValidationResultMessageHelper validationResultMessageHelper)
        {
            _mediator = mediator;
            _createValiadtor = createValiadtor;
            _updateValiadtor = updateValiadtor;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> FeatureList()
        {
            try
            {
                var values = await _mediator.Send(new GetFeatureQuery());

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading all Feature datas!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetFeature(int id)
        {
            try
            {
                var value = await _mediator.Send(new GetFeatureByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading feature data!");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateFeature(CreateFeatureCommand createFeatureCommand)
        {
            try
            {
                ValidationResult validator = _createValiadtor.Validate(createFeatureCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(createFeatureCommand);

                    return Ok("Özellik Bilgisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new feature data!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureCommand updateFeatureCommand)
        {
            try
            {
                ValidationResult validator = _updateValiadtor.Validate(updateFeatureCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(updateFeatureCommand);

                    return Ok("Özellik Bilgisi Başarıyla Güncellendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating feature data!");
            }

        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> RemoveFeature(int id)
        {
            try
            {
                await _mediator.Send(new RemoveFeatureCommand(id));

                return Ok("Özellik Bilgisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while deleting feature data!");
            }

        }
    }
}

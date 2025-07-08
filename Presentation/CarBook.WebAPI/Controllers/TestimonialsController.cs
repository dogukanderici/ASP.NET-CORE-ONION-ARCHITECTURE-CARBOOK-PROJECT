using CarBook.Application.Features.Mediator.Commands.TestimonialCommands;
using CarBook.Application.Features.Mediator.Queries.TestimonialQueries;
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
    public class TestimonialsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateTestimonialCommand> _createValidator;
        private readonly IValidator<UpdateTestimonialCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public TestimonialsController(IMediator mediator, IValidator<CreateTestimonialCommand> createValidator, IValidator<UpdateTestimonialCommand> updateValidator, IValidationResultMessageHelper validationResultMessageHelper)
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> TestimonialList()
        {
            try
            {
                var values = await _mediator.Send(new GetTestimonialQuery());

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading all testimonial datas!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetTestimonial(int id)
        {
            try
            {
                var value = await _mediator.Send(new GetTestimonialByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading testimonial data");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialCommand createTestimonialCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createTestimonialCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(createTestimonialCommand);

                    return Ok("Yorum Bilgisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new testimonial data!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialCommand updateTestimonialCommand)
        {

            try
            {
                ValidationResult validator = _updateValidator.Validate(updateTestimonialCommand);

                if (validator.IsValid)
                {
                    await _mediator.Send(updateTestimonialCommand);

                    return Ok("Yorum Bilgisi Başarıyla Güncellendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating testimonial data!");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> RemoveTestimonial(int id)
        {
            try
            {
                await _mediator.Send(new RemoveTestimonialCommand(id));

                return Ok("Yorum Bilgisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while deleting testimonial data!");
            }
        }
    }
}

using CarBook.Application.Features.CQRS.Commands.BrandCommands;
using CarBook.Application.Features.CQRS.Handlers.BrandHandlers;
using CarBook.Application.Features.CQRS.Queries.BrandQueries;
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
    public class BrandsController : ControllerBase
    {
        private readonly GetBrandQueryHandler _getBrandQueryHandler;
        private readonly GetBrandByIdQueryHandler _getBrandByIdQueryHandler;
        private readonly CreateBrandCommandHandler _createBrandCommandHandler;
        private readonly UpdateBrandCommandHandler _updateBrandCommandHandler;
        private readonly RemoveBrandCommandHandler _removeBrandCommandHandler;
        private readonly IValidator<CreateBrandCommand> _createValidator;
        private readonly IValidator<UpdateBrandCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public BrandsController(GetBrandQueryHandler getBrandQueryHandler,
            GetBrandByIdQueryHandler getBrandByIdQueryHandler,
            CreateBrandCommandHandler createBrandCommandHandler,
            UpdateBrandCommandHandler updateBrandCommandHandler,
            RemoveBrandCommandHandler removeBrandCommandHandler,
            IValidator<CreateBrandCommand> createValidator,
            IValidator<UpdateBrandCommand> updateValidator,
            IValidationResultMessageHelper validationResultMessageHelper)
        {
            _getBrandQueryHandler = getBrandQueryHandler;
            _getBrandByIdQueryHandler = getBrandByIdQueryHandler;
            _createBrandCommandHandler = createBrandCommandHandler;
            _updateBrandCommandHandler = updateBrandCommandHandler;
            _removeBrandCommandHandler = removeBrandCommandHandler;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> BrandList()
        {
            try
            {
                var values = await _getBrandQueryHandler.Handle();

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading Brand datas!");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetBrand(int id)
        {
            try
            {
                var value = await _getBrandByIdQueryHandler.Handle(new GetBrandByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while reading Brand data!");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateBrand(CreateBrandCommand createBrandCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createBrandCommand);

                if (validator.IsValid)
                {
                    await _createBrandCommandHandler.Handle(createBrandCommand);

                    return Ok("Marka Bilgisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while creating a new Brand data!");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateBrand(UpdateBrandCommand updateBrandCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updateBrandCommand);

                if (validator.IsValid)
                {
                    await _updateBrandCommandHandler.Handle(updateBrandCommand);

                    return Ok("Marka Bilgisi Başarıyla Güncellendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while updating Brand data!");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> RemoveBrand(int id)
        {
            try
            {
                await _removeBrandCommandHandler.Handle(new RemoveBrandCommand(id));

                return Ok("Marka Bilgisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while deleting Brand data!");
            }
        }
    }
}
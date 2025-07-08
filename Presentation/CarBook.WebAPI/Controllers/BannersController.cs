using CarBook.Application.Features.CQRS.Commands.BannerCommands;
using CarBook.Application.Features.CQRS.Handlers.BannerHandlers;
using CarBook.Application.Features.CQRS.Queries.BannerQueries;
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
    public class BannersController : ControllerBase
    {
        private readonly GetBannerQueryHandler _getBannerQueryHandler;
        private readonly GetBannerByIdQueryHandler _getBannerByIdQueryHandler;
        private readonly CreateBannerCommandHandler _createBannerCommandHandler;
        private readonly UpdateBannerCommandHandler _updateBannerCommandHandler;
        private readonly RemoveBannerCommandHandler _removeBannerCommandHandler;
        private readonly IValidator<CreateBannerCommand> _createValidator;
        private readonly IValidator<UpdateBannerCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public BannersController(GetBannerQueryHandler getBannerQueryHandler,
            GetBannerByIdQueryHandler getBannerByIdQueryHandler,
            CreateBannerCommandHandler createBannerCommandHandler,
            UpdateBannerCommandHandler updateBannerCommandHandler,
            RemoveBannerCommandHandler removeBannerCommandHandler,
            IValidator<CreateBannerCommand> createValidator,
            IValidator<UpdateBannerCommand> updateValidator,
            IValidationResultMessageHelper validationResultMessageHelper)
        {
            _getBannerQueryHandler = getBannerQueryHandler;
            _getBannerByIdQueryHandler = getBannerByIdQueryHandler;
            _createBannerCommandHandler = createBannerCommandHandler;
            _updateBannerCommandHandler = updateBannerCommandHandler;
            _removeBannerCommandHandler = removeBannerCommandHandler;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> BannertList()
        {
            try
            {
                var values = await _getBannerQueryHandler.Handle();

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Banner datas reading.");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetBanner(int id)
        {
            try
            {
                var value = await _getBannerByIdQueryHandler.Handle(new GetBannerByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Banner datas reading.");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateBanner(CreateBannerCommand createBannerCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createBannerCommand);

                if (validator.IsValid)
                {
                    await _createBannerCommandHandler.Handle(createBannerCommand);

                    return Ok("Banner Bilgisi Başarıyla Eklendi.");
                }
                else
                {
                    Dictionary<string, string[]> validationMessages = _validationResultMessageHelper.ValidationMessages(validator);

                    return StatusCode(400, validationMessages);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while a new Banner data adding.");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateBanner(UpdateBannerCommand updateBannerCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updateBannerCommand);

                if (validator.IsValid)
                {
                    await _updateBannerCommandHandler.Handle(updateBannerCommand);

                    return Ok("Banner Bilgisi Başarıyla Güncellendi.");
                }
                else
                {
                    Dictionary<string, string[]> validationMessages = _validationResultMessageHelper.ValidationMessages(validator);

                    return StatusCode(400, validationMessages);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Banner data updating.");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> RemoveBanner(int id)
        {
            try
            {
                await _removeBannerCommandHandler.Handle(new RemoveBannerCommand(id));

                return Ok("Banner Bilgisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Banner data deleting.");
            }
        }
    }
}

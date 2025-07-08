using CarBook.Application.Features.CQRS.Commands.BlogCategoryCommands;
using CarBook.Application.Features.CQRS.Handlers.BlogCategoryHandlers;
using CarBook.Application.Features.CQRS.Queries.BlogCategoryQueries;
using CarBook.WebAPI.Utilities.Helper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoriesController : ControllerBase
    {
        private readonly GetBlogCategoryQueryHandler _getBlogCategoryQueryHandler;
        private readonly GetBlogCategoryByIdQueryHandler _getBlogCategoryByIdQueryHandler;
        private readonly CreateBlogCategoryCommandHandler _createBlogCategoryCommandHandler;
        private readonly UpdateBlogCategoryCommandHandler _updateBlogCategoryCommandHandler;
        private readonly RemoveBlogCategoryCommandHandler _removeBlogCategoryCommandHandler;
        private readonly IValidator<CreateBlogCategoryCommand> _createValidator;
        private readonly IValidator<UpdateBlogCategoryCommand> _updateValidator;
        private readonly IValidationResultMessageHelper _validationResultMessageHelper;

        public BlogCategoriesController(GetBlogCategoryQueryHandler getBlogcategoryQueryHandler,
            GetBlogCategoryByIdQueryHandler getBlogcategoryByIdQueryHandler,
            CreateBlogCategoryCommandHandler createBlogCategoryCommandHandler,
            UpdateBlogCategoryCommandHandler updateBlogCategoryCommandHandler,
            RemoveBlogCategoryCommandHandler removeBlogCategoryCommandHandler,
            IValidator<CreateBlogCategoryCommand> createValidator,
            IValidator<UpdateBlogCategoryCommand> updateValidator,
            IValidationResultMessageHelper validationResultMessageHelper)
        {
            _getBlogCategoryQueryHandler = getBlogcategoryQueryHandler;
            _getBlogCategoryByIdQueryHandler = getBlogcategoryByIdQueryHandler;
            _createBlogCategoryCommandHandler = createBlogCategoryCommandHandler;
            _updateBlogCategoryCommandHandler = updateBlogCategoryCommandHandler;
            _removeBlogCategoryCommandHandler = removeBlogCategoryCommandHandler;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _validationResultMessageHelper = validationResultMessageHelper;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> CategoryList()
        {
            try
            {
                var values = await _getBlogCategoryQueryHandler.Handle();

                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Blog Category datas reading.");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var value = await _getBlogCategoryByIdQueryHandler.Handle(new GetBlogCategoryByIdQuery(id));

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Blog Category data reading.");
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> CreateCategory(CreateBlogCategoryCommand createBlogCategoryCommand)
        {
            try
            {
                ValidationResult validator = _createValidator.Validate(createBlogCategoryCommand);

                if (validator.IsValid)
                {
                    await _createBlogCategoryCommandHandler.Handle(createBlogCategoryCommand);

                    return Ok("Blog Kategori Bilgisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while a new Blog Category data adding.");
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> UpdateCategory(UpdateBlogCategoryCommand updateBlogCategoryCommand)
        {
            try
            {
                ValidationResult validator = _updateValidator.Validate(updateBlogCategoryCommand);

                if (validator.IsValid)
                {
                    await _updateBlogCategoryCommandHandler.Handle(updateBlogCategoryCommand);

                    return Ok("Blog Kategori Bilgisi Başarıyla Eklendi.");
                }

                return StatusCode(400, _validationResultMessageHelper.ValidationMessages(validator));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Blog Category data updating.");
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            try
            {
                await _removeBlogCategoryCommandHandler.Handle(new RemoveBlogCategoryCommand(id));

                return Ok("Blog Kategori Bilgisi Başarıyla Silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured while Blog Category data deleting.");
            }
        }
    }
}

using CarBook.Application.Features.CQRS.Commands.BlogCategoryCommands;
using CarBook.Application.Features.CQRS.Handlers.BlogCategoryHandlers;
using CarBook.Application.Features.CQRS.Queries.BlogCategoryQueries;
using Microsoft.AspNetCore.Http;
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

        public BlogCategoriesController(GetBlogCategoryQueryHandler getBlogcategoryQueryHandler,
            GetBlogCategoryByIdQueryHandler getBlogcategoryByIdQueryHandler,
            CreateBlogCategoryCommandHandler createBlogCategoryCommandHandler,
            UpdateBlogCategoryCommandHandler updateBlogCategoryCommandHandler,
            RemoveBlogCategoryCommandHandler removeBlogCategoryCommandHandler)
        {
            _getBlogCategoryQueryHandler = getBlogcategoryQueryHandler;
            _getBlogCategoryByIdQueryHandler = getBlogcategoryByIdQueryHandler;
            _createBlogCategoryCommandHandler = createBlogCategoryCommandHandler;
            _updateBlogCategoryCommandHandler = updateBlogCategoryCommandHandler;
            _removeBlogCategoryCommandHandler = removeBlogCategoryCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var values = await _getBlogCategoryQueryHandler.Handle();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var value = await _getBlogCategoryByIdQueryHandler.Handle(new GetBlogCategoryByIdQuery(id));

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateBlogCategoryCommand createBlogCategoryCommand)
        {
            await _createBlogCategoryCommandHandler.Handle(createBlogCategoryCommand);

            return Ok("Blog Kategori Bilgisi Başarıyla Eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateBlogCategoryCommand updateBlogCategoryCommand)
        {
            await _updateBlogCategoryCommandHandler.Handle(updateBlogCategoryCommand);

            return Ok("Blog Kategori Bilgisi Başarıyla Eklendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            await _removeBlogCategoryCommandHandler.Handle(new RemoveBlogCategoryCommand(id));

            return Ok("Blog Kategori Bilgisi Başarıyla Silindi.");
        }
    }
}

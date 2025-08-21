using CarBook.Dto.BlogCategoryDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.BlogCategoryServices
{
    public interface IBlogCategoryService
    {
        Task<UIServiceApiResponseSetting<ResultBlogCategoryDto>> GetBlogCategoryAsync();
        Task<UIServiceApiResponseSetting<ResultBlogCategoryDto>> GetBlogCategoryByIdAsync(int id);
        Task<HttpResponseMessage> CreateBlogCategoryAsync(CreateBlogCategoryDto createBlogCategoryDto);
        Task<HttpResponseMessage> UpdateBlogCategoryAsync(UpdateBlogCategoryDto updateBlogCategoryDto);
        Task<HttpResponseMessage> DeleteBlogCategoryAsync(int id);
    }
}

using CarBook.Dto.BlogCommentDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.BlogCommentServices
{
    public interface IBlogCommentService
    {
        Task<UIServiceApiResponseSetting<ResultBlogCommentDto>> GetBlogCommentByBlogIdAsync(Guid id);
        Task<UIServiceApiResponseSetting<ResultBlogCommentDto>> GetBlogCommentByIdAsync(Guid id);
        Task<HttpResponseMessage> CreateNewBlogCommentAsync(CreateBlogCommentDto createBlogCommentDto);
        Task<HttpResponseMessage> UpdateBlogCommentAsync(UpdateBlogCommentDto updateBlogCommentDto);
        Task<HttpResponseMessage> DeleteBlogCommentAsync(Guid id);
    }
}

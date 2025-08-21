using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Utilities.Settings;
using System.Collections.Specialized;

namespace CarBook.WebUI.Services.BlogServices
{
    public interface IBlogService
    {
        Task<int> GetBlogCountWithPublishStateAsync(bool publishState);
        Task<UIServiceApiResponseSetting<ResultBlogDto>> GetBlogWithPublishStateAsync(NameValueCollection nameValueCollection);
        Task<UIServiceApiResponseSetting<ResultBlogDto>> GetLast3BlogsAsync();
        Task<UIServiceApiResponseSetting<ResultBlogDto>> GetBlogByIdAsync(Guid id);
        Task<HttpResponseMessage> CreateNewBlogAsync(CreateBlogDto createBlogDto);
        Task<HttpResponseMessage> UpdateBlogAsync(UpdateBlogDto updateBlogDto);
        Task<HttpResponseMessage> DeleteBlogAsync(Guid id);
    }
}

using CarBook.Dto.BlogDtos;
using System.Collections.Specialized;

namespace CarBook.WebUI.Services.BlogServices
{
    public interface IBlogService
    {
        Task<int> GetBlogCountWithPublishStateAsync(bool publishState);
        Task<List<ResultBlogDto>> GetBlogWithPublishStateAsync(NameValueCollection nameValueCollection);
        Task<bool> CreateNewBlogAsync(CreateBlogDto createBlogDto);
    }
}

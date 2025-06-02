using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Models;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;

namespace CarBook.WebUI.Services.BlogServices
{
    public class BlogService : IBlogService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BlogService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<int> GetBlogCountWithPublishStateAsync(bool publishState)
        {
            HttpClient client = _httpClientFactory.CreateClient("BlogClient");
            HttpResponseMessage response = await client.GetAsync($"blogs/getblogtotalcount?publishstate={publishState}");

            int value = 0;

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<int>(jsonData);
            }

            return value;
        }

        public async Task<List<ResultBlogDto>> GetBlogWithPublishStateAsync(NameValueCollection nameValueCollection)
        {
            HttpClient client = _httpClientFactory.CreateClient("BlogClient");
            HttpResponseMessage response = await client.GetAsync($"blogs/getblogwithpublishstate?{nameValueCollection}");

            List<ResultBlogDto> values = new List<ResultBlogDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultBlogDto>>(jsonData);
            }

            return values;
        }

        public async Task<bool> CreateNewBlogAsync(CreateBlogDto createBlogDto)
        {
            throw new NotImplementedException();
        }
    }
}

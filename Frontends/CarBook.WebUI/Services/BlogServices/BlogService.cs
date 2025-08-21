using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http.Json;
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
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"blogs/getblogtotalcount?publishstate={publishState}");

            int value = 0;

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<int>(jsonData);
            }

            return value;
        }

        public async Task<UIServiceApiResponseSetting<ResultBlogDto>> GetBlogByIdAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"blogs/{id}");

            ResultBlogDto value = new ResultBlogDto();

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultBlogDto>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultBlogDto>
            {
                HttpResponseMessage = response,
                ResponseData = value
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultBlogDto>> GetBlogWithPublishStateAsync(NameValueCollection nameValueCollection)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"blogs/getblogwithpublishstate?{nameValueCollection}");

            List<ResultBlogDto> values = new List<ResultBlogDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultBlogDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultBlogDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultBlogDto>> GetLast3BlogsAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync("blogs/getlast3blogs");

            List<ResultBlogDto> values = new List<ResultBlogDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultBlogDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultBlogDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<HttpResponseMessage> CreateNewBlogAsync(CreateBlogDto createBlogDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateBlogDto>("blogs", createBlogDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateBlogAsync(UpdateBlogDto updateBlogDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateBlogDto>("blogs", updateBlogDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteBlogAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"blogs/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpResponseMessage deletedDataResponse = await client.DeleteAsync($"blogs?id={id}");

                return deletedDataResponse;
            }

            return response;
        }
    }
}

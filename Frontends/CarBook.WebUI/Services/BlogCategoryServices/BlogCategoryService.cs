using CarBook.Dto.BlogCategoryDtos;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace CarBook.WebUI.Services.BlogCategoryServices
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BlogCategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIServiceApiResponseSetting<ResultBlogCategoryDto>> GetBlogCategoryAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync("blogcategories");

            List<ResultBlogCategoryDto> values = new List<ResultBlogCategoryDto>();

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultBlogCategoryDto>>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultBlogCategoryDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultBlogCategoryDto>> GetBlogCategoryByIdAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"blogcategories/{id}");

            ResultBlogCategoryDto value = new ResultBlogCategoryDto();

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultBlogCategoryDto>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultBlogCategoryDto>
            {
                HttpResponseMessage = response,
                ResponseData = value
            };
        }
        public async Task<HttpResponseMessage> CreateBlogCategoryAsync(CreateBlogCategoryDto createBlogCategoryDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateBlogCategoryDto>("blogcategories", createBlogCategoryDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateBlogCategoryAsync(UpdateBlogCategoryDto updateBlogCategoryDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateBlogCategoryDto>("blogcategories", updateBlogCategoryDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteBlogCategoryAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"blogcategories/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpResponseMessage deletedDataResponse = await client.DeleteAsync($"blogcategories?id={id}");

                return deletedDataResponse;
            }

            return response;
        }
    }
}

using CarBook.Dto.BlogCommentDtos;
using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace CarBook.WebUI.Services.BlogCommentServices
{
    public class BlogCommentService : IBlogCommentService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BlogCommentService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIServiceApiResponseSetting<ResultBlogCommentDto>> GetBlogCommentByBlogIdAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"blogcomments/getblogcommentbyblogid?id={id}");

            List<ResultBlogCommentDto> values = new List<ResultBlogCommentDto>();

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultBlogCommentDto>>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultBlogCommentDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultBlogCommentDto>> GetBlogCommentByIdAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"blogcomments/{id}");

            ResultBlogCommentDto value = new ResultBlogCommentDto();

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultBlogCommentDto>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultBlogCommentDto>
            {
                HttpResponseMessage = response,
                ResponseData = value
            };
        }

        public async Task<HttpResponseMessage> CreateNewBlogCommentAsync(CreateBlogCommentDto createBlogCommentDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateBlogCommentDto>("blogcomments", createBlogCommentDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateBlogCommentAsync(UpdateBlogCommentDto updateBlogCommentDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateBlogCommentDto>("blogcomments", updateBlogCommentDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteBlogCommentAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"blogcomments/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpResponseMessage deletedDataResponse = await client.DeleteAsync($"blogcomments?id={id}");

                return deletedDataResponse;
            }

            return response;
        }
    }
}

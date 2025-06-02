using CarBook.Dto.BlogCommentDtos;
using CarBook.Dto.BlogDtos;
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

        public async Task<bool> CreateNewBlogCommentAsync(CreateBlogCommentDto createBlogCommentDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("BlogCommentClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateBlogCommentDto>("blogcomments", createBlogCommentDto);

            if (response.IsSuccessStatusCode)
            {
                string responseMessage = await response.Content.ReadAsStringAsync();

                return true;
            }

            return false;
        }
    }
}

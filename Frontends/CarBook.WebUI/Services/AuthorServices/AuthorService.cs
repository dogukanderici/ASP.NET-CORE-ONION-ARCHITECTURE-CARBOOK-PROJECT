using CarBook.Dto.AuthorDtos;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;

namespace CarBook.WebUI.Services.AuthorServices
{
    public class AuthorService : IAuthorService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthorService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIServiceApiResponseSetting<ResultAuthorDto>> GetAuthorAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync("authors");

            List<ResultAuthorDto> values = new List<ResultAuthorDto>();

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultAuthorDto>>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultAuthorDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultAuthorDto>> GetAuthorByIdAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"authors/{id}");

            ResultAuthorDto value = new ResultAuthorDto();

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultAuthorDto>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultAuthorDto>
            {
                HttpResponseMessage = response,
                ResponseData = value
            };
        }
        public async Task<HttpResponseMessage> CreateAuthorAsync(CreateAuthorDto createAuthorDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateAuthorDto>("authors", createAuthorDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateAuthorAsync(UpdateAuthorDto updateAuthorDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateAuthorDto>("authors", updateAuthorDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteAuthorAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"authors/{id}");

            if (response.IsSuccessStatusCode)
            {
                HttpResponseMessage deletedDataResponse = await client.DeleteAsync($"authors?id={id}");

                return deletedDataResponse;
            }

            return response;
        }
    }
}

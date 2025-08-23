using CarBook.Dto.CarReviewDtos;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;

namespace CarBook.WebUI.Services.CarReviewServices
{
    public class CarReviewService : ICarReviewService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarReviewService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIServiceApiResponseSetting<ResultCarReviewDto>> GetCarReviewAsync()
        {

            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");

            HttpResponseMessage response = await client.GetAsync("carreviews");

            List<ResultCarReviewDto> values = new List<ResultCarReviewDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultCarReviewDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultCarReviewDto>
            {
                ResponseDatas = values,
                HttpResponseMessage = response
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultCarReviewDto>> GetCarReviewByIdAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"carreviews/{id}");

            string responseData = await response.Content.ReadAsStringAsync();

            ResultCarReviewDto value = new ResultCarReviewDto();

            if (response.IsSuccessStatusCode)
            {
                value = JsonConvert.DeserializeObject<ResultCarReviewDto>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultCarReviewDto>
            {
                ResponseData = value,
                HttpResponseMessage = response
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultCarReviewDto>> GetCarReviewByCarIdAsync(int id, bool? status)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"carreviews/carreviewwithcar/{id}/{status}");

            string responseData = await response.Content.ReadAsStringAsync();

            List<ResultCarReviewDto> values = new List<ResultCarReviewDto>();

            if (response.IsSuccessStatusCode)
            {
                values = JsonConvert.DeserializeObject<List<ResultCarReviewDto>>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultCarReviewDto>
            {
                ResponseDatas = values,
                HttpResponseMessage = response
            };
        }

        public async Task<HttpResponseMessage> CreateCarReviewAsync(CreateCarReviewDto createCarReviewDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateCarReviewDto>("carreviews", createCarReviewDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateCarReviewAsync(UpdateCarReviewDto updateCarReviewDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateCarReviewDto>("carreviews", updateCarReviewDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteCarReviewAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"carreviews/{id}");

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                ResultCarReviewDto result = JsonConvert.DeserializeObject<ResultCarReviewDto>(responseData);

                if (result != null)
                {
                    HttpResponseMessage deleteDataResponse = await client.DeleteAsync($"carreviews?id={id}");

                    return deleteDataResponse;
                }
                else
                {
                    return response;
                }
            }
            else
            {
                return response;
            }
        }
    }
}

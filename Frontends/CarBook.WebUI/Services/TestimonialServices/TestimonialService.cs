using CarBook.Dto.TestimonialDtos;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;
using System.Reflection;

namespace CarBook.WebUI.Services.TestimonialServices
{
    public class TestimonialService : ITestimonialService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestimonialService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIServiceApiResponseSetting<ResultTestimonialDto>> GetTestimonialAsync()
        {

            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");

            HttpResponseMessage response = await client.GetAsync("testimonials");

            List<ResultTestimonialDto> values = new List<ResultTestimonialDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultTestimonialDto>
            {
                ResponseDatas = values,
                HttpResponseMessage = response
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultTestimonialDto>> GetTestimonialByIdAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"testimonials/{id}");

            string responseData = await response.Content.ReadAsStringAsync();

            ResultTestimonialDto value = new ResultTestimonialDto();

            if (response.IsSuccessStatusCode)
            {
                value = JsonConvert.DeserializeObject<ResultTestimonialDto>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultTestimonialDto>
            {
                ResponseData = value,
                HttpResponseMessage = response
            };
        }

        public async Task<HttpResponseMessage> CreateTestimonialAsync(CreateTestimonialDto createTestimonialDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateTestimonialDto>("testimonials", createTestimonialDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateTestimonialAsync(UpdateTestimonialDto updateTestimonialDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateTestimonialDto>("testimonials", updateTestimonialDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteTestimonialAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"testimonials/{id}");

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                ResultTestimonialDto result = JsonConvert.DeserializeObject<ResultTestimonialDto>(responseData);

                if (result != null)
                {
                    HttpResponseMessage deleteDataResponse = await client.DeleteAsync($"testimonials?id={id}");

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

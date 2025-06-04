using CarBook.Dto.TestimonialDtos;
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

        public async Task<List<ResultTestimonialDto>> GetTestimonialsAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("TestimonialClient");
            HttpResponseMessage response = await client.GetAsync("testimonials");

            List<ResultTestimonialDto> values = new List<ResultTestimonialDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(jsonData);
            }

            return values;
        }
    }
}


using CarBook.Dto.FooterAddressDtos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace CarBook.WebUI.Services.FooterAddressServices
{
    public class FooterAddressService : IFooterAddressService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FooterAddressService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ResultFooterAddressDto>> GetFooterAddressAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("FooterAddressClient");
            HttpResponseMessage response = await client.GetAsync("footeraddresses");

            List<ResultFooterAddressDto> values = new List<ResultFooterAddressDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultFooterAddressDto>>(jsonData);
            }

            return values;
        }
    }
}


using CarBook.Dto.FooterAddressDtos;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
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

        public async Task<UIServiceApiResponseSetting<ResultFooterAddressDto>> GetFooterAddressAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync("footeraddresses");

            List<ResultFooterAddressDto> values = new List<ResultFooterAddressDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultFooterAddressDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultFooterAddressDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultFooterAddressDto>> GetFooterAddressByIdAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"footeraddresses/{id}");

            ResultFooterAddressDto value = new ResultFooterAddressDto();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultFooterAddressDto>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultFooterAddressDto>
            {
                HttpResponseMessage = response,
                ResponseData = value
            };
        }

        public async Task<HttpResponseMessage> CreateFooterAddressAsync(CreateFooterAddressDto createFooterAddressDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateFooterAddressDto>("footeraddresses", createFooterAddressDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateFooterAddressAsync(UpdateFooterAddressDto updateFooterAddressDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateFooterAddressDto>("footeraddresses", updateFooterAddressDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteFooterAddressAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"footeraddresses/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpResponseMessage deletedDataResponse = await client.DeleteAsync($"footeraddresses?id={id}");

                return deletedDataResponse;
            }

            return response;
        }
    }
}

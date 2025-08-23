using CarBook.Dto.PricingTypeDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace CarBook.WebUI.Services.PricingTypeServices
{
    public class PricingTypeService : IPricingTypeService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PricingTypeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIServiceApiResponseSetting<ResultPricingTypeDto>> GetPricingTypeAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync("pricingtypes");

            List<ResultPricingTypeDto> values = new List<ResultPricingTypeDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultPricingTypeDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultPricingTypeDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultPricingTypeDto>> GetPricingTypeByIdAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"pricingtypes/{id}");

            ResultPricingTypeDto value = new ResultPricingTypeDto();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultPricingTypeDto>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultPricingTypeDto>
            {
                HttpResponseMessage = response,
                ResponseData = value
            };
        }

        public async Task<HttpResponseMessage> CreatePricingTypeAsync(CreatePricingTypeDto createPricingTypeDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreatePricingTypeDto>("pricingtypes", createPricingTypeDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdatePricingTypeAsync(UpdatePricingTypeDto updatePricingTypeDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdatePricingTypeDto>("pricingtypes", updatePricingTypeDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeletePricingTypeAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"pricingtypes/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpResponseMessage deletedDataResponse = await client.DeleteAsync($"pricingtypes?id={id}");

                return deletedDataResponse;
            }

            return response;
        }
    }
}

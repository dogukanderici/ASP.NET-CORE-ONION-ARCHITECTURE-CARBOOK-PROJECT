using CarBook.Dto.FooterAddressDtos;
using CarBook.Dto.SocialMediaDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _UILayoutFooterComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public _UILayoutFooterComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessageForAddress = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/footeraddresses");
            var responseMessageForSocialMedia = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/socialmedias");

            FooterUIViewModel model = new FooterUIViewModel();

            if (responseMessageForAddress.IsSuccessStatusCode)
            {
                var jsonData = await responseMessageForAddress.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultFooterAddressDto>>(jsonData);

                model.FooterAddressUIViewModel.FooterAddressDatas = value;
            }

            if (responseMessageForSocialMedia.IsSuccessStatusCode)
            {
                var jsonData = await responseMessageForSocialMedia.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultSocialMediaDto>>(jsonData);

                model.SocialMediaUIViewModel.ResultDatas = value;
            }

            return View(model);
        }
    }
}

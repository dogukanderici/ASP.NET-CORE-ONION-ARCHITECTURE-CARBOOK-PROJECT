using CarBook.Dto.FooterAddressDtos;
using CarBook.Dto.SocialMediaDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.FooterAddressServices;
using CarBook.WebUI.Services.SocialMediaServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarBook.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _UILayoutFooterComponentPartial : ViewComponent
    {
        private readonly IFooterAddressService _footerAddressService;
        private readonly ISocialMediaService _socialMediaService;

        public _UILayoutFooterComponentPartial(IFooterAddressService footerAddressService, ISocialMediaService socialMediaService)
        {
            _footerAddressService = footerAddressService;
            _socialMediaService = socialMediaService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ResultFooterAddressDto> valuesForFooterAddress = await _footerAddressService.GetFooterAddressAsync();
            List<ResultSocialMediaDto> responseMessageForSocialMedia = await _socialMediaService.GetSocialMediaAsync();

            FooterUIViewModel model = new FooterUIViewModel();

            model.FooterAddressUIViewModel.FooterAddressDatas = valuesForFooterAddress;
            model.SocialMediaUIViewModel.ResultDatas = responseMessageForSocialMedia;

            return View(model);
        }
    }
}

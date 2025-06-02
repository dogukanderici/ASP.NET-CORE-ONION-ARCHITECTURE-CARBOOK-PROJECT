using CarBook.Dto.BannerDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.BannerServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarBook.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultCoverComponentPartial : ViewComponent
    {
        private readonly IBannerService _bannerService;

        public _DefaultCoverComponentPartial(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ResultBannerDto> values = await _bannerService.GetBannerAsync();

            BannerUIViewModel model = new BannerUIViewModel();

            model.BannerDatas = values;

            return View(model);
        }
    }
}

using CarBook.Dto.AboutDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.AboutServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarBook.WebUI.ViewComponents.AboutViewComponents
{
    public class _AboutUsComponentPartial : ViewComponent
    {
        private readonly IAboutService _aboutService;

        public _AboutUsComponentPartial(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            UIServiceApiResponseSetting<ResultAboutDto> serviceResult = await _aboutService.GetAboutAsync();

            AboutUIViewModel model = new AboutUIViewModel();

            if (serviceResult.ResponseDatas.Count() > 0)
            {
                model.AboutDatas = serviceResult.ResponseDatas;
            }

            return View(model);
        }
    }
}
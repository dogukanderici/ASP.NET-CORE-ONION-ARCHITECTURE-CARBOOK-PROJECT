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
            List<ResultAboutDto> values = await _aboutService.GetAboutAsync();

            AboutUIViewModel model = new AboutUIViewModel();

            if (values.Count() > 0)
            {
                model.AboutDatas = values;
            }

            return View(model);
        }
    }
}
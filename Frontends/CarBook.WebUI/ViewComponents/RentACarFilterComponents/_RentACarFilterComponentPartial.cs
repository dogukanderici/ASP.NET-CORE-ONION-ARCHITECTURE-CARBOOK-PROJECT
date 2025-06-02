using CarBook.Dto.LocationDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.LocationServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace CarBook.WebUI.ViewComponents.RentACarFilterComponents
{
    public class _RentACarFilterComponentPartial : ViewComponent
    {
        private readonly ILocationService _locationService;

        public _RentACarFilterComponentPartial(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.LocationList = await GetLocationListAsync();

            return View();
        }

        private async Task<List<SelectListItem>> GetLocationListAsync()
        {
            List<ResultLocationDto> values = await _locationService.GetLocationsAsync();

            List<SelectListItem> locationList = new List<SelectListItem>();

            if (values.Count() > 0)
            {
                locationList = (from item in values
                                select new SelectListItem
                                {
                                    Text = item.LocationName,
                                    Value = item.LocationID.ToString()
                                }).ToList();
            }

            return locationList;
        }
    }
}

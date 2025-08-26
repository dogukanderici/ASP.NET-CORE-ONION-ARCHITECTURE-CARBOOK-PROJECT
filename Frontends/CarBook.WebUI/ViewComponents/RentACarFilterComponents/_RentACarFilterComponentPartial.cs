using CarBook.Dto.LocationDtos;
using CarBook.WebUI.Services.LocationServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            UIServiceApiResponseSetting<ResultLocationDto> serviceResponse = await _locationService.GetLocationAsync();

            List<SelectListItem> locationList = new List<SelectListItem>();

            if (serviceResponse.ResponseDatas.Count() > 0)
            {
                locationList = (from item in serviceResponse.ResponseDatas
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

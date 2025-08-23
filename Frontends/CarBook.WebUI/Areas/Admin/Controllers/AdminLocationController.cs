using CarBook.Dto.LocationDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.LocationServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Location")]
    public class AdminLocationController : AdminBaseController
    {
        private readonly ILocationService _locationService;

        public AdminLocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UIServiceApiResponseSetting<ResultLocationDto> serviceResponse = await _locationService.GetLocationAsync();

            AdminUILocationViewModel model = new AdminUILocationViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.ResultDatas = serviceResponse.ResponseDatas;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
            }

            return View(model);
        }

        [HttpGet("Create")]
        public IActionResult CreateLocation()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateLocation(AdminUILocationViewModel adminUILocationViewModel)
        {
            HttpResponseMessage serviceResponse = await _locationService.CreateLocationAsync(adminUILocationViewModel.CreateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminLocation", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUILocationViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateLocation(int id)
        {
            UIServiceApiResponseSetting<ResultLocationDto> serviceResponse = await _locationService.GetLocationByIdAsync(id);

            AdminUILocationViewModel model = new AdminUILocationViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                string jsonData = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
                UpdateLocationDto value = JsonConvert.DeserializeObject<UpdateLocationDto>(jsonData);

                model.UpdateData = value;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateLocation(AdminUILocationViewModel adminUILocationViewModel)
        {
            HttpResponseMessage serviceResponse = await _locationService.UpdateLocationAsync(adminUILocationViewModel.UpdateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminLocation", new { area = "Admin" });
            }
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUILocationViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            HttpResponseMessage serviceResponse = await _locationService.DeleteLocationAsync(id);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (!serviceResponse.IsSuccessStatusCode)
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return RedirectToAction("Index", "AdminLocation", new { area = "Admin" });
        }
    }
}

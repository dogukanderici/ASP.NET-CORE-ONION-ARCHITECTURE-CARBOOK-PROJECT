using CarBook.Dto.ServiceDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.OurServiceServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarBook.WebUI.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IOurServiceService _ourService;

        public ServiceController(IOurServiceService ourService)
        {
            _ourService = ourService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PageRouteTitle = "Hizmetlerimiz";

            UIServiceApiResponseSetting<ResultServiceDto> serviceResponse = await _ourService.GetServiceAsync();

            ServiceUIViewModel model = new ServiceUIViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.ServiceDatas = serviceResponse.ResponseDatas;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
            }

            return View(model);
        }
    }
}

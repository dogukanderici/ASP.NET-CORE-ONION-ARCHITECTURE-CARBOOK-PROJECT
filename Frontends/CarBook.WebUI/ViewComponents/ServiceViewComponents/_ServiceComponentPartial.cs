using CarBook.Dto.ServiceDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.OurServiceServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarBook.WebUI.ViewComponents.ServiceViewComponents
{
    public class _ServiceComponentPartial : ViewComponent
    {
        private readonly IOurServiceService _ourService;

        public _ServiceComponentPartial(IOurServiceService ourService)
        {
            _ourService = ourService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

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

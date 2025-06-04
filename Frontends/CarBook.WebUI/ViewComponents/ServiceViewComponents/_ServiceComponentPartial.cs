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

            List<ResultServiceDto> values = await _ourService.GetServicesAsync();

            ServiceUIViewModel model = new ServiceUIViewModel();

            model.ServiceDatas = values;

            return View(model);
        }
    }
}

using CarBook.Dto.CarFeatureDtos;
using CarBook.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.CarDetailViewComponents
{
    public class _CarDetailCarFeatureByCarIdComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(string carFeatures)
        {
            CarUIViewModel model = new CarUIViewModel();
            model.CarData.CarFeatures = JsonConvert.DeserializeObject<List<ResultCarFeatureForCarDto>>(carFeatures);

            return View(model);
        }
    }
}

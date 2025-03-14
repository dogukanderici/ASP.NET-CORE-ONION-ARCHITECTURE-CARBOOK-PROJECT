using CarBook.Dto.CarPricingDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUICarPricingViewModel
    {
        public List<ResultCarPricingDto> ResultDatas { get; set; }
        public List<ResultCarPricingForCarDto> ResultForCarDatas { get; set; }
        public CreateCarPricingDto CreateData { get; set; }
        public UpdateCarPricingDto UpdateData { get; set; }

        public List<SelectListItem> PricingTypes { get; set; }
    }
}

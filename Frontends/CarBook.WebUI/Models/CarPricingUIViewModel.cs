using CarBook.Dto.CarPricingDtos;

namespace CarBook.WebUI.Models
{
    public class CarPricingUIViewModel
    {
        public CarPricingUIViewModel()
        {
            CarPricingDatas = new List<ResultCarPricingDto>();
            CarPricingForCarDatas = new List<ResultCarPricingForCarDto>();
        }

        public List<ResultCarPricingDto> CarPricingDatas { get; set; }
        public List<ResultCarPricingForCarDto> CarPricingForCarDatas { get; set; }
    }
}

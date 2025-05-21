using CarBook.Dto.CarPricingDtos;

namespace CarBook.WebUI.Models
{
    public class CarPricingUIViewModel
    {
        public CarPricingUIViewModel()
        {
            CarPricingDatas = new List<ResultCarPricingDto>();
        }

        public List<ResultCarPricingDto> CarPricingDatas { get; set; }
    }
}

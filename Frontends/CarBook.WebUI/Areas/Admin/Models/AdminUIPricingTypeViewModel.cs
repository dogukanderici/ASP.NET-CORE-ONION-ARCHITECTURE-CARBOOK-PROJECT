using CarBook.Dto.PricingTypeDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUIPricingTypeViewModel
    {
        public List<ResultPricingTypeDto> ResultDatas { get; set; }
        public CreatePricingTypeDto CreateData { get; set; }
        public UpdatePricingTypeDto UpdateData { get; set; }
    }
}

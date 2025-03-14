using CarBook.Dto.CarFeatureDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUICarFeatureViewModel
    {
        public List<ResultCarFeatureDto> CarFeatureDatas { get; set; }
        public ResultCarFeatureDto CarFeatureData { get; set; }
        public List<ResultCarFeatureForCarDto> CarFeatureForCarDatas { get; set; }
        public CreateCarFeatureDto CreateCarFeatureData { get; set; }
        public List<CreateCarFeatureDto> CreateCarFeatureDatas { get; set; }
        public UpdateCarFeatureDto UpdateCarFeatureData { get; set; }
    }
}

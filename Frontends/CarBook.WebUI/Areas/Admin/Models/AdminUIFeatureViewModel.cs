using CarBook.Dto.FeatureDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUIFeatureViewModel
    {
        public List<ResultFeatureDto> FeatureDatas { get; set; }
        public CreateFeatureDto CreateDatas { get; set; }
        public UpdateFeatureDto UpdateDatas { get; set; }
    }
}

using CarBook.Dto.BrandDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUIBrandViewModel
    {
        public List<ResultBrandDto> ResultDatas { get; set; }
        public CreateBrandDto CreateData { get; set; }
        public UpdateBrandDto UpdateData { get; set; }
    }
}

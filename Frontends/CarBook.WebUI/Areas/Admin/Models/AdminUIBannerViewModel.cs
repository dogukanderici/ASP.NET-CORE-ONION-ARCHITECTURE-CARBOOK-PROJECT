using CarBook.Dto.BannerDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUIBannerViewModel
    {
        public List<ResultBannerDto> ResultDatas { get; set; }
        public CreateBannerDto CreateData { get; set; }
        public UpdateBannerDto UpdateData { get; set; }
    }
}

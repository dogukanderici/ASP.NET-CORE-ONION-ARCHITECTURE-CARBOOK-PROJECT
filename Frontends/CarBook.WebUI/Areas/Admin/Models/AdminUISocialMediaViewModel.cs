using CarBook.Dto.SocialMediaDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUISocialMediaViewModel
    {
        public List<ResultSocialMediaDto> ResultDatas { get; set; }
        public CreateSocialMediaDto CreateData { get; set; }
        public UpdateSocialMediaDto UpdateData { get; set; }
    }
}

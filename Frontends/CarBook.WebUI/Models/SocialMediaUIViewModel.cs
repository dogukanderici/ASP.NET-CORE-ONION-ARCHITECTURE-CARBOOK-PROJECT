using CarBook.Dto.SocialMediaDtos;

namespace CarBook.WebUI.Models
{
    public class SocialMediaUIViewModel
    {
        public SocialMediaUIViewModel()
        {
            ResultDatas = new List<ResultSocialMediaDto>();
        }

        public List<ResultSocialMediaDto> ResultDatas { get; set; }
    }
}

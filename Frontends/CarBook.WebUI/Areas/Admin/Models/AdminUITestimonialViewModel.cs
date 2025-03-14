using CarBook.Dto.TestimonialDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUITestimonialViewModel
    {
        public List<ResultTestimonialDto> ResultDatas { get; set; }
        public CreateTestimonialDto CreateData { get; set; }
        public UpdateTestimonialDto UpdateData { get; set; }
    }
}

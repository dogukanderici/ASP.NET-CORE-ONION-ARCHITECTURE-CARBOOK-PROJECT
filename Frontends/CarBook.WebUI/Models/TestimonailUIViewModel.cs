using CarBook.Dto.TestimonialDtos;

namespace CarBook.WebUI.Models
{
    public class TestimonailUIViewModel
    {
        public TestimonailUIViewModel()
        {
            TestimonialDatas = new List<ResultTestimonialDto>();
        }

        public List<ResultTestimonialDto> TestimonialDatas { get; set; }
    }
}

using CarBook.Dto.TestimonialDtos;

namespace CarBook.WebUI.Services.TestimonialServices
{
    public interface ITestimonialService
    {
        Task<List<ResultTestimonialDto>> GetTestimonialsAsync();
    }
}

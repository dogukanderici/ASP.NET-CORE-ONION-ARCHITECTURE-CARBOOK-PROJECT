using CarBook.Dto.TestimonialDtos;
using CarBook.Dto.TestimonialDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.TestimonialServices
{
    public interface ITestimonialService
    {
        Task<UIServiceApiResponseSetting<ResultTestimonialDto>> GetTestimonialAsync();
        Task<UIServiceApiResponseSetting<ResultTestimonialDto>> GetTestimonialByIdAsync(int id);
        Task<HttpResponseMessage> CreateTestimonialAsync(CreateTestimonialDto createTestimonialDto);
        Task<HttpResponseMessage> UpdateTestimonialAsync(UpdateTestimonialDto updateTestimonialDto);
        Task<HttpResponseMessage> DeleteTestimonialAsync(int id);
    }
}

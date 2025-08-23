using CarBook.Dto.CarReviewDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.CarReviewServices
{
    public interface ICarReviewService
    {
        Task<UIServiceApiResponseSetting<ResultCarReviewDto>> GetCarReviewAsync();
        Task<UIServiceApiResponseSetting<ResultCarReviewDto>> GetCarReviewByIdAsync(Guid id);
        Task<UIServiceApiResponseSetting<ResultCarReviewDto>> GetCarReviewByCarIdAsync(int id, bool? status);
        Task<HttpResponseMessage> CreateCarReviewAsync(CreateCarReviewDto createCarReviewDto);
        Task<HttpResponseMessage> UpdateCarReviewAsync(UpdateCarReviewDto updateCarReviewDto);
        Task<HttpResponseMessage> DeleteCarReviewAsync(int id);
    }
}

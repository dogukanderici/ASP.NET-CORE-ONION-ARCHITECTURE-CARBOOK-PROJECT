using CarBook.Dto.AboutDtos;

namespace CarBook.WebUI.Services.AboutServices
{
    public interface IAboutService
    {
        Task<(List<ResultAboutDto>, HttpResponseMessage)> GetAboutAsync();
        Task<(ResultAboutDto, HttpResponseMessage)> GetAboutByIdAsync(int id);
        Task<HttpResponseMessage> CreateAboutAsync(CreateAboutDto createAboutDto);
        Task<HttpResponseMessage> UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        Task<HttpResponseMessage> DeleteAboutAsync(int id);
    }
}

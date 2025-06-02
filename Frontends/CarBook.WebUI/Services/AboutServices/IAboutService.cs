using CarBook.Dto.AboutDtos;

namespace CarBook.WebUI.Services.AboutServices
{
    public interface IAboutService
    {
        Task<List<ResultAboutDto>> GetAboutAsync();
    }
}

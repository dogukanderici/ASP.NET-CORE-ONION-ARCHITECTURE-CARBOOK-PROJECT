using CarBook.Dto.AuthorDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.AuthorServices
{
    public interface IAuthorService
    {
        Task<UIServiceApiResponseSetting<ResultAuthorDto>> GetAuthorAsync();
        Task<UIServiceApiResponseSetting<ResultAuthorDto>> GetAuthorByIdAsync(Guid id);
        Task<HttpResponseMessage> CreateAuthorAsync(CreateAuthorDto createAuthorDto);
        Task<HttpResponseMessage> UpdateAuthorAsync(UpdateAuthorDto updateAuthorDto);
        Task<HttpResponseMessage> DeleteAuthorAsync(Guid id);
    }
}

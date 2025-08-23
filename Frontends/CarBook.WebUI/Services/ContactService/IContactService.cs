using CarBook.Dto.ContactDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.ContactService
{
    public interface IContactService
    {
        Task<UIServiceApiResponseSetting<ResultContactDto>> GetContactInboxAsync();
        Task<UIServiceApiResponseSetting<ResultContactDto>> GetContactOutboxAsync();
        Task<UIServiceApiResponseSetting<ResultContactDto>> GetContactByIdAsync(Guid id);
        Task<HttpResponseMessage> AddNewContactMessageForUI(CreateContactDto createContactDto);
        Task<HttpResponseMessage> DeleteContactAsync(Guid id);
    }
}

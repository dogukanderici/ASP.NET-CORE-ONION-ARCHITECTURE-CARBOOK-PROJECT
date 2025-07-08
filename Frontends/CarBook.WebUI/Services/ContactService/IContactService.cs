using CarBook.Dto.ContactDtos;

namespace CarBook.WebUI.Services.ContactService
{
    public interface IContactService
    {
        Task<HttpResponseMessage> AddNewContactMessageForUI(CreateContactDto createContactDto);
    }
}

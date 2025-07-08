using CarBook.Dto.ContactDtos;

namespace CarBook.WebUI.Services.ContactService
{
    public class ContactService : IContactService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> AddNewContactMessageForUI(CreateContactDto createContactDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateContactDto>("contacts", createContactDto);

            return response;
        }
    }
}

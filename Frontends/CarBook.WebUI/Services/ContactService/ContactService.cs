using CarBook.Dto.ContactDtos;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;
using System.Net;

namespace CarBook.WebUI.Services.ContactService
{
    public class ContactService : IContactService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIServiceApiResponseSetting<ResultContactDto>> GetContactInboxAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync("contacts/inbox");

            List<ResultContactDto> values = new List<ResultContactDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultContactDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultContactDto>> GetContactOutboxAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync("contacts/outbox");

            List<ResultContactDto> values = new List<ResultContactDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultContactDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultContactDto>> GetContactByIdAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"contacts/{id}");

            ResultContactDto value = new ResultContactDto();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultContactDto>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultContactDto>
            {
                HttpResponseMessage = response,
                ResponseData = value
            };
        }

        public async Task<HttpResponseMessage> AddNewContactMessageForUI(CreateContactDto createContactDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateContactDto>("contacts", createContactDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteContactAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"contacts/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpResponseMessage deletedDataResponse = await client.DeleteAsync($"contacts?id={id}");

                return deletedDataResponse;
            }

            return response;
        }
    }
}

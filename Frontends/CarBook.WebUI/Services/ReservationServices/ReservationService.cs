using CarBook.Dto.CarDtos;
using CarBook.Dto.ReservationDtos;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarBook.WebUI.Services.ReservationServices
{
    public class ReservationService : IReservationService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> CreateReservationForUI(CreateReservationDto createReservationDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateReservationDto>("reservations", createReservationDto);

            return response;
        }

        public async Task<List<ResultReservationDto>> GetRerservationByEmail(string email)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"reservations/reservationbyemail/{email}");

            List<ResultReservationDto> values = new List<ResultReservationDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultReservationDto>>(jsonData);
            }

            return values;
        }
    }
}

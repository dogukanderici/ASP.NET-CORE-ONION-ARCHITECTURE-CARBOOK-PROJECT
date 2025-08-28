using CarBook.Dto.CarDtos;
using CarBook.Dto.ReservationDtos;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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

        public async Task<ResultReservationDto> GetReservationById(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"reservations/{id}");

            ResultReservationDto value = new ResultReservationDto();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultReservationDto>(jsonData);
            }

            return value;
        }

        public async Task<HttpResponseMessage> CreateReservationForUI(CreateReservationDto createReservationDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateReservationDto>("reservations", createReservationDto);

            return response;
        }
    }
}

using CarBook.Dto.StatisticsDtos;

namespace CarBook.WebUI.Services.StatisticsServices
{
    public interface IStatisticsService
    {
        Task<int> GetCarCountAsync();
        Task<int> GetLocationCountAsync();
        Task<int> GetBrandCountAsync();
        Task<int> GetCarCountByFuelElectricAsync();
    }
}

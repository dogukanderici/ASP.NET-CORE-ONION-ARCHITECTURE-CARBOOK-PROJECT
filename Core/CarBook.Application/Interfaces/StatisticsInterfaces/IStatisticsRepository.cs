using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Interfaces.StatisticsInterfaces
{
    public interface IStatisticsRepository
    {
        int GetCarCount();
        int GetLocationCount();
        int GetAuthorCount();
        int GetBlogCount();
        int GetBrandCount();
        decimal GetAvgRentPriceForHourlyCount();
        decimal GetAvgRentPriceForDailyCount();
        decimal GetAvgRentPriceForWeeklyCount();
        decimal GetAvgRentPriceForMountlyCount();
        int GetCarCountByTransmissionIsAuto();
        string GetBrandNameByMaxCar();
        string GetBlogTitleByMaxBlogComment();
        int GetCarCountByKmSmallerThan1000();
        int GetCarCountByFuelGasOrDiesel();
        int GetCarCountByFuelElectric();
        string GetCarBrandAndModelByPriceDailyMax();
        string GetCarBrandAndModelByPriceDailyMin();
    }
}

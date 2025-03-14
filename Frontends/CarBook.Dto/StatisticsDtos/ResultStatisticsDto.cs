using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.StatisticsDtos
{
    public class ResultStatisticsDto
    {
        public int AuthorCount { get; set; }
        public int BlogCount { get; set; }
        public int BrandCount { get; set; }
        public int CarCountByFuelElectric { get; set; }
        public int CarCountByFuelGasOrDiesel { get; set; }
        public int CarCountByKmSmallerThan1000 { get; set; }
        public int CarCountByTransmissionIsAuto { get; set; }
        public int CarCount { get; set; }
        public int LocationCount { get; set; }
        public decimal AvgRentPriceForDailyCount { get; set; }
        public decimal AvgRentPriceForHourlyCount { get; set; }
        public decimal AvgRentPriceForMountlyCount { get; set; }
        public decimal AvgRentPriceForWeeklyCount { get; set; }
        public string BlogTitleByMaxBlogComment { get; set; }
        public string BrandNameByMaxCar { get; set; }
        public string CarBrandAndModelByPriceDailyMax { get; set; }
        public string CarBrandAndModelByPriceDailyMin { get; set; }
    }
}

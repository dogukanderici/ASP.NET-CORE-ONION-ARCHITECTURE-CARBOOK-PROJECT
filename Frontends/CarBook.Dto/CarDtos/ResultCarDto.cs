using CarBook.Dto.BrandDtos;
using CarBook.Dto.CarFeatureDtos;
using CarBook.Dto.CarPricingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.CarDtos
{
    public class ResultCarDto
    {
        public int CarID { get; set; }
        public int BrandID { get; set; }
        public ResultBrandDto Brand { get; set; }
        public string Model { get; set; }
        public string CoverImageURL { get; set; }
        public decimal KM { get; set; }
        public string Transmission { get; set; }
        public int Seat { get; set; }
        public int Luggage { get; set; }
        public string Fuel { get; set; }
        public string BigImageURL { get; set; }
        public List<ResultCarPricingForCarDto> CarPricings { get; set; }
        public List<ResultCarFeatureForCarDto> CarFeatures { get; set; }
    }
}

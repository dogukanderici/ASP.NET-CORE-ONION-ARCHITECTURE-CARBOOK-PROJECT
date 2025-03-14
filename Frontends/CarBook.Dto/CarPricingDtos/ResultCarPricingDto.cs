using CarBook.Dto.CarDtos;
using CarBook.Dto.PricingTypeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.CarPricingDtos
{
    public class ResultCarPricingDto
    {
        public int CarPricingID { get; set; }
        public int CarID { get; set; }
        public ResultCarDto Car { get; set; }
        public int PricingTypeID { get; set; }
        public ResultPricingTypeDto PricingType { get; set; }
        public decimal Amount { get; set; }
    }
}
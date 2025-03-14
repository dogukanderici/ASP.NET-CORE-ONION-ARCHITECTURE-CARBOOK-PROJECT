using CarBook.Dto.PricingTypeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.CarPricingDtos
{
    public class UpdateCarPricingDto
    {
        public int CarPricingID { get; set; }
        public int CarID { get; set; }
        public int PricingTypeID { get; set; }
        public decimal Amount { get; set; }
    }
}

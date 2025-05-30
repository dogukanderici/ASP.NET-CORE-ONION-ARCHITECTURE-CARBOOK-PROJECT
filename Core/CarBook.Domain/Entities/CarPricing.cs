﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Domain.Entities
{
    public class CarPricing
    {
        [Key]
        public int CarPricingID { get; set; }
        public int CarID { get; set; }
        public Car Car { get; set; }
        public int PricingTypeID { get; set; }
        public PricingType PricingType { get; set; }
        public decimal Amount { get; set; }
    }
}

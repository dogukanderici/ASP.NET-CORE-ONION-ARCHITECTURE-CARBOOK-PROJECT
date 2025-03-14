using CarBook.Application.Features.CQRS.Results.CarResults;
using CarBook.Application.Features.Mediator.Results.PricingTypeResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Results.CarPricingResult
{
    public class GetCarPricingQueryResult
    {
        public int CarPricingID { get; set; }
        public int CarID { get; set; }
        public GetCarQueryResult Car { get; set; }
        public int PricingTypeID { get; set; }
        public GetPricingTypeQueryResult PricingType { get; set; }
        public decimal Amount { get; set; }
    }
}

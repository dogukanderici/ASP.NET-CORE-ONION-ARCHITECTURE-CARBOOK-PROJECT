using CarBook.Application.Features.CQRS.Results.BrandResults;
using CarBook.Application.Features.CQRS.Results.CarFeatureResults;
using CarBook.Application.Features.CQRS.Results.CarPricingResult;
using CarBook.Application.Features.CQRS.Results.CarResults;
using CarBook.Application.Features.Mediator.Results.LocationResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Results.RentACarResults
{
    public class GetRentACarForCarQueryResult
    {
        public Guid RentACarID { get; set; }
        public int LocationID { get; set; }
        public GetLocationQueryResult Location { get; set; }
        public bool Available { get; set; }
        public DateTimeOffset PickUpDate { get; set; }
        public DateTimeOffset DropOffDate { get; set; }
    }
}

using CarBook.Application.Features.CQRS.Handlers.CarPricingHandlers;
using CarBook.Application.Features.CQRS.Results.BrandResults;
using CarBook.Application.Features.CQRS.Results.CarFeatureResults;
using CarBook.Application.Features.CQRS.Results.CarPricingResult;
using CarBook.Application.Features.Mediator.Results.RentACarResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Results.CarResults
{
    public class GetCarQueryResult
    {
        public int CarID { get; set; }
        public int BrandID { get; set; }
        public GetBrandByIdQueryResult Brand { get; set; }
        public string Model { get; set; }
        public string CoverImageURL { get; set; }
        public decimal KM { get; set; }
        public string Transmission { get; set; }
        public int Seat { get; set; }
        public int Luggage { get; set; }
        public string Fuel { get; set; }
        public string BigImageURL { get; set; }

        public List<GetCarPricingForCarQueryResult> CarPricings { get; set; }
        public List<GetCarFeatureForCarQueryResult> CarFeatures { get; set; }
        public List<GetRentACarForCarQueryResult> RentACars { get; set; }
    }
}

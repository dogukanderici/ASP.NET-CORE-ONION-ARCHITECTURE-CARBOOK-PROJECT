using CarBook.Application.Features.CQRS.Results.BrandResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Results.CarResults
{
    public class GetCarForCarFeatureQueryResult
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Domain.Entities
{
    public class Car
    {
        public int CarID { get; set; }
        public int BrandID { get; set; }
        public Brand Brand { get; set; }

        public string Model { get; set; }
        public string CoverImageURL { get; set; }
        public decimal KM { get; set; }
        public string Transmission { get; set; }
        public int Seat { get; set; }
        public int Luggage { get; set; }
        public string Fuel { get; set; }
        public string BigImageURL { get; set; }

        public List<CarFeature> CarFeatures { get; set; }
        public CarDescription CarDescription { get; set; }
        public List<CarPricing> CarPricings { get; set; }
        public RentACar RentACar { get; set; }
        public List<CarReview> CarReviews { get; set; }
    }
}

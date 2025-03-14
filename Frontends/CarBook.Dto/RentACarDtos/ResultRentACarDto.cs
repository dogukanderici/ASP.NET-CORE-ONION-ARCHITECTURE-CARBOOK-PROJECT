using CarBook.Dto.CarDtos;
using CarBook.Dto.LocationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.RentACarDtos
{
    public class ResultRentACarDto
    {
        public Guid RentACarID { get; set; }
        public int LocationID { get; set; }
        public ResultLocationDto Location { get; set; }
        public int CarID { get; set; }
        public ResultCarForRentACarDto Car { get; set; }
        public bool Available { get; set; }
        public DateTimeOffset PickUpDate { get; set; }
        public DateTimeOffset DropOffDate { get; set; }
    }
}

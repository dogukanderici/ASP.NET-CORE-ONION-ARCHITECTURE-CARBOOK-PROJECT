using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Domain.Entities
{
    public class Reservation
    {
        [Key]
        public Guid ReservationID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int CarID { get; set; }
        public int PickUpLocationID { get; set; }
        public int DropOffLocationID { get; set; }
        public int Age { get; set; }
        public int DriverLicenceAge { get; set; }
        public string Description { get; set; }
        public DateTimeOffset PickUpDate { get; set; }
        public DateTimeOffset DropOffDate { get; set; }
        public Location PickUpLocation { get; set; }
        public Location DropOffLocation { get; set; }
        public RentACar RentACar { get; set; }
    }
}

using CarBook.Application.Features.CQRS.Results.CarResults;
using CarBook.Application.Features.Mediator.Results.LocationResults;
using CarBook.Application.Features.Mediator.Results.ReservationResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Results.RentACarResults
{
    public class GetRentACarQueryResult
    {
        public Guid RentACarID { get; set; }
        public int LocationID { get; set; }
        public GetLocationQueryResult Location { get; set; }
        public int CarID { get; set; }
        public GetCarForRentACarQueryResult Car { get; set; }
        public bool Available { get; set; }
        public Guid ReservationID { get; set; }
        public GetReservationForRentACarQueryResult Reservation { get; set; }
        public DateTimeOffset PickUpDate { get; set; }
        public DateTimeOffset DropOffDate { get; set; }
    }
}

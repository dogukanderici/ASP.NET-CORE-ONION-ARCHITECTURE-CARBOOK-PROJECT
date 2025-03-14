using CarBook.Application.Features.Mediator.Results.RentACarResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Queries.RentACarQueries
{
    public class GetRentACarByAvailablityQuery : IRequest<List<GetRentACarByAvailablityQueryResult>>
    {
        public int LocationID { get; set; }
        public bool Available { get; set; }
        public DateTimeOffset PickUpDate { get; set; }
        public DateTimeOffset DropOffDate { get; set; }

        public GetRentACarByAvailablityQuery(int locationID, bool available, DateTimeOffset pickUpDate, DateTimeOffset dropOffDate)
        {
            LocationID = locationID;
            Available = available;
            PickUpDate = pickUpDate;
            DropOffDate = dropOffDate;
        }
    }
}

using CarBook.Application.Features.CQRS.Results.CarResults;
using CarBook.Application.Features.Mediator.Results.RentACarResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Commands.RentACarCommands
{
    public class UpdateRentACarCommand: IRequest
    {
        public Guid RentACarID { get; set; }
        public int LocationID { get; set; }
        public int CarID { get; set; }
        public bool Available { get; set; }
        public DateTimeOffset PickUpDate { get; set; }
        public DateTimeOffset DropOffDate { get; set; }
    }
}

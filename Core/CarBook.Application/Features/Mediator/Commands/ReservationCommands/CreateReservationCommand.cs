﻿using CarBook.Configurations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Commands.ReservationCommands
{
    public class CreateReservationCommand : IRequest<HandlerResponseOptions>
    {
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
    }
}

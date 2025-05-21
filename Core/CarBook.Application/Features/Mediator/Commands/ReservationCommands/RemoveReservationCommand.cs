using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Commands.ReservationCommands
{
    public class RemoveReservationCommand : IRequest
    {
        public Guid Id { get; set; }

        public RemoveReservationCommand(Guid id)
        {
            Id = id;
        }
    }
}

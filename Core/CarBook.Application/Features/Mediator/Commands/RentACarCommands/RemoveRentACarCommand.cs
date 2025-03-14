using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Commands.RentACarCommands
{
    public class RemoveRentACarCommand : IRequest
    {
        public Guid Id { get; set; }

        public RemoveRentACarCommand(Guid id)
        {
            Id = id;
        }
    }
}

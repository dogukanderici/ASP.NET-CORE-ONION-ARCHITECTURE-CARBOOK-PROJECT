using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Commands.TagCloudCommands
{
    public class RemoveTagCloudCommand : IRequest
    {
        public Guid Id { get; set; }

        public RemoveTagCloudCommand(Guid id)
        {
            Id = id;
        }
    }
}

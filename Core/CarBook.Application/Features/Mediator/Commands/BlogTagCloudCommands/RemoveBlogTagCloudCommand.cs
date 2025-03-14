using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Commands.BlogTagCloudCommands
{
    public class RemoveBlogTagCloudCommand : IRequest
    {
        public Guid Id { get; set; }

        public RemoveBlogTagCloudCommand(Guid id)
        {
            Id = id;
        }
    }
}

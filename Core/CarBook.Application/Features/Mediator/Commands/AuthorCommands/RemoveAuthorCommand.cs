using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Commands.AuthorCommands
{
    public class RemoveAuthorCommand : IRequest
    {
        public Guid Id { get; set; }
        public RemoveAuthorCommand(Guid id)
        {
            Id = id;
        }
    }
}

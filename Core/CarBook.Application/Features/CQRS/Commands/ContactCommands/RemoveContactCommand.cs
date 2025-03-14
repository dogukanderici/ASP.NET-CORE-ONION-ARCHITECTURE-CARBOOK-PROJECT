using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Commands.ContactCommands
{
    public class RemoveContactCommand
    {
        public Guid Id { get; set; }

        public RemoveContactCommand(Guid id)
        {
            Id = id;
        }
    }
}

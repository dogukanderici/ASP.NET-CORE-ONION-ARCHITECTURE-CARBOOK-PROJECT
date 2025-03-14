using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Commands.CarFeatureCommands
{
    public class RemoveCarFeatureCommand
    {
        public int Id { get; set; }

        public RemoveCarFeatureCommand(int id)
        {
            Id = id;
        }
    }
}

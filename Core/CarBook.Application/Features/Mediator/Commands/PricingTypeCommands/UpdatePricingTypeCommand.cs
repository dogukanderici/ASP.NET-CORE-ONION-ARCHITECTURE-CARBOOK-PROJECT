using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Commands.PricingTypeCommands
{
    public class UpdatePricingTypeCommand : IRequest
    {
        public int PricingTypeID { get; set; }
        public string Name { get; set; }
    }
}

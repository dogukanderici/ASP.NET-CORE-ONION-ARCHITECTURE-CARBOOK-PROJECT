using CarBook.Application.Features.CQRS.Results.CarResults;
using CarBook.Application.Features.Mediator.Results.PricingTypeResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Commands.CarPricingCommands
{
    public class CreateCarPricingCommand
    {
        public int CarID { get; set; }
        public int PricingTypeID { get; set; }
        public decimal Amount { get; set; }
    }
}

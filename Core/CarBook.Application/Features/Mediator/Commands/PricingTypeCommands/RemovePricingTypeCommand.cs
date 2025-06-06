﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Commands.PricingTypeCommands
{
    public class RemovePricingTypeCommand : IRequest
    {
        public int Id { get; set; }

        public RemovePricingTypeCommand(int id)
        {
            Id = id;
        }
    }
}

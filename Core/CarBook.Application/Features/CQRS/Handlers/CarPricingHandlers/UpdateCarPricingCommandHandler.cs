using AutoMapper;
using CarBook.Application.Features.CQRS.Commands.CarPricingCommands;
using CarBook.Application.Interfaces;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.CarPricingHandlers
{
    public class UpdateCarPricingCommandHandler
    {
        private readonly IRepository<CarPricing> _repository;
        private readonly IMapper _mapper;

        public UpdateCarPricingCommandHandler(IRepository<CarPricing> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateCarPricingCommand updateCarPricingCommand)
        {
            CarPricing valueFromDto = _mapper.Map<CarPricing>(updateCarPricingCommand);

            await _repository.UpdateAsync(valueFromDto);
        }
    }
}

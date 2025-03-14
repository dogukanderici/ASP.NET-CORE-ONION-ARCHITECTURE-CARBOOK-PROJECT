using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.PricingTypeCommands;
using CarBook.Application.Interfaces;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.PricingTypeHandlers
{
    public class CreatePricingTypeCommandHandler : IRequestHandler<CreatePricingTypeCommand>
    {
        private readonly IRepository<PricingType> _repository;
        private readonly IMapper _mapper;

        public CreatePricingTypeCommandHandler(IRepository<PricingType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreatePricingTypeCommand request, CancellationToken cancellationToken)
        {
            PricingType valueFromDto = _mapper.Map<PricingType>(request);

            await _repository.CreateAsync(valueFromDto);
        }
    }
}

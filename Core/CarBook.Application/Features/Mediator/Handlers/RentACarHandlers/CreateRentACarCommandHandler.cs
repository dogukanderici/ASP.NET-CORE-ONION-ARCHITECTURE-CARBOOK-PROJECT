using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.RentACarCommands;
using CarBook.Application.Interfaces;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.RentACarHandlers
{
    public class CreateRentACarCommandHandler : IRequestHandler<CreateRentACarCommand>
    {
        private readonly IRepository<RentACar> _repository;
        private readonly IMapper _mapper;

        public CreateRentACarCommandHandler(IRepository<RentACar> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateRentACarCommand request, CancellationToken cancellationToken)
        {
            RentACar valueFromDto = _mapper.Map<RentACar>(request);

            await _repository.CreateAsync(valueFromDto);
        }
    }
}

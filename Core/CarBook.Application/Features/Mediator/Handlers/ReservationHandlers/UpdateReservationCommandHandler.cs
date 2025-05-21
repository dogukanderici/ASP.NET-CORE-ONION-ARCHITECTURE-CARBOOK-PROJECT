using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.ReservationCommands;
using CarBook.Application.Interfaces;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.ReservationHandlers
{
    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;

        public UpdateReservationCommandHandler(IRepository<Reservation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            Reservation valueToDto = _mapper.Map<Reservation>(request);

            await _repository.UpdateAsync(valueToDto);
        }
    }
}

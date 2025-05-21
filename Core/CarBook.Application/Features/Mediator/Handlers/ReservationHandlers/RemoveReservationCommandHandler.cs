using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.ReservationCommands;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.ReservationHandlers
{
    public class RemoveReservationCommandHandler : IRequestHandler<RemoveReservationCommand>
    {
        private readonly IRepository<Reservation> _repository;

        public RemoveReservationCommandHandler(IRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveReservationCommand request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Reservation> dbQueryOptions = new DbQueryOptions<Reservation>();

            Expression<Func<Reservation, bool>> filter = x => x.ReservationID == request.Id;

            dbQueryOptions.filter = filter;

            Reservation value = await _repository.GetByIdAsync(dbQueryOptions);

            if (value != null)
            {
                await _repository.RemoveAsync(value);
            }
        }
    }
}

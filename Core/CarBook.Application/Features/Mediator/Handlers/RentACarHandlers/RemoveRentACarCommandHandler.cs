using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.RentACarCommands;
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

namespace CarBook.Application.Features.Mediator.Handlers.RentACarHandlers
{
    public class RemoveRentACarCommandHandler : IRequestHandler<RemoveRentACarCommand>
    {
        private readonly IRepository<RentACar> _repository;

        public RemoveRentACarCommandHandler(IRepository<RentACar> repository)
        {
            _repository = repository;
        }
        public async Task Handle(RemoveRentACarCommand request, CancellationToken cancellationToken)
        {
            DbQueryOptions<RentACar> dbQueryOpitons = new DbQueryOptions<RentACar>();

            Expression<Func<RentACar, bool>> filter = x => x.RentACarID == request.Id;

            dbQueryOpitons.filter = filter;

            RentACar value = await _repository.GetByIdAsync(dbQueryOpitons);

            if (value != null)
            {
                await _repository.RemoveAsync(value);
            }
        }
    }
}

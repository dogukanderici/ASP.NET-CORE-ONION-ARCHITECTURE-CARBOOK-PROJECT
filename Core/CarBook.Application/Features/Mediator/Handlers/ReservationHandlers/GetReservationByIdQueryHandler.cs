using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.ReservationQueries;
using CarBook.Application.Features.Mediator.Results.ReservationResults;
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
    public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, GetReservationByIdQueryResult>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;

        public GetReservationByIdQueryHandler(IRepository<Reservation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetReservationByIdQueryResult> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Reservation> dbQueryOptions = new DbQueryOptions<Reservation>();

            Expression<Func<Reservation, bool>> filter = x => x.ReservationID == request.Id;

            dbQueryOptions.filter = filter;

            Reservation values = await _repository.GetByIdAsync(dbQueryOptions);

            GetReservationByIdQueryResult valueToDto = _mapper.Map<GetReservationByIdQueryResult>(values);

            return valueToDto;
        }
    }
}

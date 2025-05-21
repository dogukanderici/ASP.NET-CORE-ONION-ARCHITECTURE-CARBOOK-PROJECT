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
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.ReservationHandlers
{
    public class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, List<GetReservationQueryResult>>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;

        public GetReservationQueryHandler(IRepository<Reservation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetReservationQueryResult>> Handle(GetReservationQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Reservation> dbQueryOptions = new DbQueryOptions<Reservation>();

            List<Reservation> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetReservationQueryResult> valueToDto = _mapper.Map<List<GetReservationQueryResult>>(values);

            return valueToDto;
        }
    }
}

using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.ReservationQueries;
using CarBook.Application.Features.Mediator.Results.ReservationResults;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace CarBook.Application.Features.Mediator.Handlers.ReservationHandlers
{
    public class GetReservationByEmailQueryHandler : IRequestHandler<GetReservationByEmailQuery, List<GetReservationByEmailQueryResult>>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;

        public GetReservationByEmailQueryHandler(IRepository<Reservation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetReservationByEmailQueryResult>> Handle(GetReservationByEmailQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Reservation> dbQueryoptions = new DbQueryOptions<Reservation>();

            Expression<Func<Reservation, bool>> filter = x => x.Email == request.Email;

            Expression<Func<Reservation, object>> shorting = x => x.PickUpDate;

            Dictionary<Expression<Func<Reservation, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<Reservation, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        r=>r.Car,
                        new List<Expression<Func<object, object>>>
                        {
                            b=>((Car)b).Brand
                        }
                    },
                };

            dbQueryoptions.filter = filter;
            dbQueryoptions.thenIncludes = thenIncludes;
            dbQueryoptions.shorting = shorting;
            dbQueryoptions.shortingType = "descending";

            List < Reservation > values = await _repository.GetAllAsync(dbQueryoptions);

            List<GetReservationByEmailQueryResult> valuesToDto = _mapper.Map<List<GetReservationByEmailQueryResult>>(values);

            return valuesToDto;
        }
    }
}

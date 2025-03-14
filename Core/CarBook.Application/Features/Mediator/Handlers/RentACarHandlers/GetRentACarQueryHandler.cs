using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.RentACarQueries;
using CarBook.Application.Features.Mediator.Results.RentACarResults;
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
    public class GetRentACarQueryHandler : IRequestHandler<GetRentACarQuery, List<GetRentACarQueryResult>>
    {
        private readonly IRepository<RentACar> _repository;
        private readonly IMapper _mapper;

        public GetRentACarQueryHandler(IRepository<RentACar> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetRentACarQueryResult>> Handle(GetRentACarQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<RentACar> dbQueryOptions = new DbQueryOptions<RentACar>();

            Dictionary<Expression<Func<RentACar, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<RentACar, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        r=>r.Car,
                        new List<Expression<Func<object, object>>>
                        {
                            c=>((Car)c).Brand,
                        }
                    },
                    {
                        r=>r.Car,
                        new List<Expression<Func<object, object>>>
                        {
                            c=>((Car)c).CarPricings,
                            c=>((CarPricing)c).PricingType,
                        }
                    },
                    {
                        r=>r.Location,
                        new List<Expression<Func<object, object>>>{}
                    }

                };

            dbQueryOptions.thenIncludes = thenIncludes;

            List<RentACar> values = await _repository.GetAllAsync(dbQueryOptions);
            List<GetRentACarQueryResult> valueToDto = _mapper.Map<List<GetRentACarQueryResult>>(values);

            return valueToDto;
        }
    }
}

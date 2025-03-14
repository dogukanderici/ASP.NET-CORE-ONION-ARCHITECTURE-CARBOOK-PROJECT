using AutoMapper;
using CarBook.Application.Features.CQRS.Queries.CarPricingQueries;
using CarBook.Application.Features.CQRS.Queries.CarPricingQueries;
using CarBook.Application.Features.CQRS.Results.CarPricingResult;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.CarPricingHandlers
{
    public class GetCarPricingByIdQueryHandler
    {
        private readonly IRepository<CarPricing> _repository;
        private readonly IMapper _mapper;

        public GetCarPricingByIdQueryHandler(IRepository<CarPricing> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetCarPricingQueryResult> Handle(GetCarPricingByIdQuery getCarPricingByIdQuery)
        {
            DbQueryOptions<CarPricing> dbQueryOptions = new DbQueryOptions<CarPricing>();

            Expression<Func<CarPricing, bool>> filter = x => x.CarPricingID == getCarPricingByIdQuery.Id;
            Dictionary<Expression<Func<CarPricing, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<CarPricing, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        cp=>cp.Car,
                        new List<Expression<Func<object, object>>>
                        {
                            d=>((Car)d).Brand
                        }
                    },
                    {
                        cp=>cp.PricingType,
                        new List<Expression<Func<object, object>>>{}
                    }
                };

            dbQueryOptions.filter = filter;
            dbQueryOptions.thenIncludes = thenIncludes;

            CarPricing value = await _repository.GetByIdAsync(dbQueryOptions);

            GetCarPricingQueryResult valueToDto = _mapper.Map<GetCarPricingQueryResult>(value);

            return valueToDto;
        }

    }
}

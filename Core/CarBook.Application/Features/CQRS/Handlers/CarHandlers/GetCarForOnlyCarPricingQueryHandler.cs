using AutoMapper;
using CarBook.Application.Features.CQRS.Results.CarResults;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.CarHandlers
{
    public class GetCarForOnlyCarPricingQueryHandler
    {
        private readonly IRepository<Car> _repository;
        private readonly IMapper _mapper;

        public GetCarForOnlyCarPricingQueryHandler(IRepository<Car> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetCarForOnlyCarPricingQueryResult>> Handle()
        {
            DbQueryOptions<Car> dbQueryOptions = new DbQueryOptions<Car>();

            Dictionary<Expression<Func<Car, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<Car, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        x=>x.Brand,
                        new List<Expression<Func<object, object>>>{}
                    },
                    {
                        x=>x.CarPricings,
                        new List<Expression<Func<object, object>>>
                        {
                            y=>((CarPricing)y).PricingType
                        }
                    }

                };

            dbQueryOptions.thenIncludes = thenIncludes;

            List<Car> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetCarForOnlyCarPricingQueryResult> valueToDto = _mapper.Map<List<GetCarForOnlyCarPricingQueryResult>>(values);

            return valueToDto;

        }
    }
}

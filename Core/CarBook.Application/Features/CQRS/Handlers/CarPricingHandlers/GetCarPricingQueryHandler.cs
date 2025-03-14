using AutoMapper;
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
    public class GetCarPricingQueryHandler
    {
        private readonly IRepository<CarPricing> _repository;
        private readonly IMapper _mapper;

        public GetCarPricingQueryHandler(IRepository<CarPricing> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetCarPricingQueryResult>> Handle()
        {
            DbQueryOptions<CarPricing> dbQueryOptions = new DbQueryOptions<CarPricing>();

            Dictionary<Expression<Func<CarPricing, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<CarPricing, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        cp=>cp.Car,
                        new List<Expression<Func<object, object>>>
                        {
                            d => ((Car)d).Brand
                        }
                    },
                    {
                        cp=>cp.PricingType,
                        new List<Expression<Func<object, object>>>()
                    }
                };

            Expression<Func<CarPricing, object>> shorting = cp => cp.CarPricingID;

            dbQueryOptions.thenIncludes = thenIncludes;
            dbQueryOptions.shortingType = "ascending";
            dbQueryOptions.shorting = shorting;

            List<CarPricing> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetCarPricingQueryResult> valueToDto = _mapper.Map<List<GetCarPricingQueryResult>>(values);

            return valueToDto;
        }
    }
}

using AutoMapper;
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
    public class GetCarPricingByCarIdQueryHandler
    {
        private readonly IRepository<CarPricing> _repository;
        private readonly IMapper _mapper;

        public GetCarPricingByCarIdQueryHandler(IRepository<CarPricing> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetCarPricingForCarQueryResult>> Handle(GetCarPricingByCarIdQuery getCarPricingByCarIdQuery)
        {
            DbQueryOptions<CarPricing> dbQueryOptions = new DbQueryOptions<CarPricing>();

            Expression<Func<CarPricing, bool>> filter = cp => cp.CarID == getCarPricingByCarIdQuery.Id;
            Dictionary<Expression<Func<CarPricing, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<CarPricing, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        cp=>cp.PricingType,
                        new List<Expression<Func<object, object>>>()
                    }
                };

            dbQueryOptions.filter = filter;
            dbQueryOptions.thenIncludes = thenIncludes;

            List<CarPricing> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetCarPricingForCarQueryResult> valueToDto = _mapper.Map<List<GetCarPricingForCarQueryResult>>(values);

            return valueToDto;
        }
    }
}

using AutoMapper;
using CarBook.Application.Features.CQRS.Results.CarResults;
using CarBook.Application.Interfaces;
using CarBook.Application.Interfaces.CarInterfaces;
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
    public class GetLast5CarsQueryHandler
    {
        private readonly IRepository<Car> _repository;
        private readonly IMapper _mapper;

        public GetLast5CarsQueryHandler(IRepository<Car> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetLast5CarsQueryResult>> Handle()
        {
            Dictionary<Expression<Func<Car, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<Car, object>>, List<System.Linq.Expressions.Expression<Func<object, object>>>>
                {
                    {
                        c=>c.Brand,
                        new List<Expression<Func<object, object>>>{}
                    },
                    {
                        c=>c.CarPricings,
                        new List<Expression<Func<object, object>>>
                        {
                            d=>((CarPricing)d).PricingType
                        }
                    }
                };

            Expression<Func<Car, object>> shorting = x => x.CarID;

            DbQueryOptions<Car> dbQueryOptions = new DbQueryOptions<Car>();

            dbQueryOptions.thenIncludes = thenIncludes;
            dbQueryOptions.shortingType = "descending";
            dbQueryOptions.shorting = shorting;
            dbQueryOptions.DataTakeNumber = 5;

            List<Car> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetLast5CarsQueryResult> valueToDto = _mapper.Map<List<GetLast5CarsQueryResult>>(values);

            return valueToDto;
        }
    }
}

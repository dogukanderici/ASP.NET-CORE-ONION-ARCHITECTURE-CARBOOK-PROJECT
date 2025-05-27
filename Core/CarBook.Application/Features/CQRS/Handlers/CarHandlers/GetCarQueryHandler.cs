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
    public class GetCarQueryHandler
    {
        private readonly IRepository<Car> _repository;
        private readonly IMapper _mapper;

        public GetCarQueryHandler(IRepository<Car> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetCarQueryResult>> Handle()
        {
            Dictionary<Expression<Func<Car, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<Car, object>>, List<Expression<Func<object, object>>>>
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
                    },
                    {
                        c=>c.CarFeatures,
                        new List<Expression<Func<object, object>>>
                        {
                            cf=>((CarFeature)cf).Feature
                        }
                    },
                    //{
                    //    c=>c.RentACar,
                    //    new List<Expression<Func<object, object>>>{
                    //        r=>((RentACar)r).Location
                    //    }
                    //}
                };

            DbQueryOptions<Car> dbQueryOptions = new DbQueryOptions<Car>();

            dbQueryOptions.thenIncludes = thenIncludes;

            List<Car> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetCarQueryResult> valueToDto = _mapper.Map<List<GetCarQueryResult>>(values);

            return valueToDto;
        }
    }
}

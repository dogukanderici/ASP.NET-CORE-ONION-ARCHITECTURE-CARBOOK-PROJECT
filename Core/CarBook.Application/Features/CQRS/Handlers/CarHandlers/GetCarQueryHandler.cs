using AutoMapper;
using CarBook.Application.Features.CQRS.Queries.CarQueries;
using CarBook.Application.Features.CQRS.Results.CarResults;
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

namespace CarBook.Application.Features.CQRS.Handlers.CarHandlers
{
    public class GetCarQueryHandler
    {
        private readonly IRepository<Car> _repository;
        private readonly IMapper _mapper;
        private readonly GetCarCountQueryHandler _carCountQueryHandler;

        public GetCarQueryHandler(IRepository<Car> repository, IMapper mapper, GetCarCountQueryHandler carCountQueryHandler)
        {
            _repository = repository;
            _mapper = mapper;
            _carCountQueryHandler = carCountQueryHandler;
        }

        public async Task<GetCarDataQueryResult> Handle(GetCarQuery request)
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

            if (request.TakeNumber > 0)
            {
                dbQueryOptions.SkipNumber = request.SkipNumber;
                dbQueryOptions.DataTakeNumber = request.TakeNumber;
            }

            int carDataCount = await _carCountQueryHandler.Handle();

            List<Car> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetCarQueryResult> valueToDto = _mapper.Map<List<GetCarQueryResult>>(values);

            GetCarDataQueryResult data = new GetCarDataQueryResult();

            data.CarDatas = valueToDto;
            data.TotalDataCount = carDataCount;

            return data;
        }
    }
}

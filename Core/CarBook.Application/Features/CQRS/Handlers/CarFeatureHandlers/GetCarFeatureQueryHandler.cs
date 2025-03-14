using AutoMapper;
using CarBook.Application.Features.CQRS.Results.CarFeatureResults;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.CarFeatureHandlers
{
    public class GetCarFeatureQueryHandler
    {
        private readonly IRepository<CarFeature> _repository;
        private readonly IMapper _mapper;

        public GetCarFeatureQueryHandler(IRepository<CarFeature> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetCarFeatureQueryResult>> Handle()
        {
            DbQueryOptions<CarFeature> dbQueryOptions = new DbQueryOptions<CarFeature>();

            Dictionary<Expression<Func<CarFeature, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<CarFeature, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        cf=>cf.Car,
                        new List<Expression<Func<object, object>>>
                        {
                            cb=>((Car)cb).Brand
                        }
                    },
                    {
                        cf=>cf.Feature,
                        new List<Expression<Func<object, object>>>{}
                    }
                };

            dbQueryOptions.thenIncludes = thenIncludes;

            List<CarFeature> values = await _repository.GetAllAsync(dbQueryOptions);
            List<GetCarFeatureQueryResult> valueToDto = _mapper.Map<List<GetCarFeatureQueryResult>>(values);

            return valueToDto;
        }
    }
}

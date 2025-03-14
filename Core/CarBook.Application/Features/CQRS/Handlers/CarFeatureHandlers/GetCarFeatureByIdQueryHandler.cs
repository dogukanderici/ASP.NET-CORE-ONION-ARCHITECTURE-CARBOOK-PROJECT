using AutoMapper;
using CarBook.Application.Features.CQRS.Queries.CarFeatureQueries;
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
    public class GetCarFeatureByIdQueryHandler
    {
        private readonly IRepository<CarFeature> _repository;
        private readonly IMapper _mapper;

        public GetCarFeatureByIdQueryHandler(IRepository<CarFeature> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetCarFeatureByIdQueryResult> Handle(GetCarFeatureByIdQuery getcarFeatureByIdQuery)
        {
            DbQueryOptions<CarFeature> dbQueryOptions = new DbQueryOptions<CarFeature>();

            Expression<Func<CarFeature, bool>> filter = cf => cf.CarFeatureID == getcarFeatureByIdQuery.Id;
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

            dbQueryOptions.filter = filter;
            dbQueryOptions.thenIncludes = thenIncludes;

            CarFeature value = await _repository.GetByIdAsync(dbQueryOptions);
            GetCarFeatureByIdQueryResult valueToDto = _mapper.Map<GetCarFeatureByIdQueryResult>(value);

            return valueToDto;
        }
    }
}

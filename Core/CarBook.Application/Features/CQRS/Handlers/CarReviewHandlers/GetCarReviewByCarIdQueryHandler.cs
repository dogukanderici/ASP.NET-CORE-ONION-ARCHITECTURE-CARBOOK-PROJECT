using AutoMapper;
using CarBook.Application.Features.CQRS.Queries.CarReviewQueries;
using CarBook.Application.Features.CQRS.Results.CarReviewResults;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.CarReviewHandlers
{
    public class GetCarReviewByCarIdQueryHandler
    {
        private readonly IRepository<CarReview> _repository;
        private readonly IMapper _mapper;

        public GetCarReviewByCarIdQueryHandler(IRepository<CarReview> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetCarReviewByCarIdQueryResult>> Handle(GetCarReviewByCarIdQuery getCarReviewByCarIdQuery)
        {
            DbQueryOptions<CarReview> dbQueryOptions = new DbQueryOptions<CarReview>();

            Expression<Func<CarReview, bool>> filter = x => x.CarID == getCarReviewByCarIdQuery.CarID;

            List<Expression<Func<CarReview, object>>> includes = new List<Expression<Func<CarReview, object>>>
            {
                x=>x.Car
            };

            dbQueryOptions.filter = filter;
            dbQueryOptions.includes = includes;

            List<CarReview> values = await _repository.GetAllAsync(dbQueryOptions);

            return _mapper.Map<List<GetCarReviewByCarIdQueryResult>>(values);
        }
    }
}

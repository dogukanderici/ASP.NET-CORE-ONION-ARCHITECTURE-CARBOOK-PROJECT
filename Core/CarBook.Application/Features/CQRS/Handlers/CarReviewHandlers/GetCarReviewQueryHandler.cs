using AutoMapper;
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
    public class GetCarReviewQueryHandler
    {
        private readonly IRepository<CarReview> _repository;
        private readonly IMapper _mapper;

        public GetCarReviewQueryHandler(IRepository<CarReview> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetCarReviewQueryResult>> Handle()
        {
            DbQueryOptions<CarReview> dbQueryOptions = new DbQueryOptions<CarReview>();

            List<Expression<Func<CarReview, object>>> includes = new List<Expression<Func<CarReview, object>>>
            {
                x=>x.Car
            };

            dbQueryOptions.includes = includes;

            List<CarReview> values = await _repository.GetAllAsync(dbQueryOptions);

            return _mapper.Map<List<GetCarReviewQueryResult>>(values);
        }
    }
}

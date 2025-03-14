using AutoMapper;
using CarBook.Application.Features.CQRS.Results.CategoryResults;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.BlogCategoryHandlers
{
    public class GetBlogCategoryQueryHandler
    {
        private readonly IRepository<BlogCategory> _repository;
        private readonly IMapper _mapper;

        public GetBlogCategoryQueryHandler(IRepository<BlogCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBlogCategoryQueryResult>> Handle()
        {
            Expression<Func<BlogCategory, bool>> filter = bc => bc.CategoryStatus == true;
            Expression<Func<BlogCategory, object>> shorting = bc => bc.BlogCategoryID;

            DbQueryOptions<BlogCategory> dbQueryOptions = new DbQueryOptions<BlogCategory>();
            dbQueryOptions.filter = filter;
            dbQueryOptions.shorting = shorting;

            List<BlogCategory> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetBlogCategoryQueryResult> valueToDto = _mapper.Map<List<GetBlogCategoryQueryResult>>(values);

            return valueToDto;
        }
    }
}

using AutoMapper;
using CarBook.Application.Features.CQRS.Queries.BlogCategoryQueries;
using CarBook.Application.Features.CQRS.Results.BlogCategoryResults;
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
    public class GetBlogCategoryByIdQueryHandler
    {
        private readonly IRepository<BlogCategory> _repository;
        private readonly IMapper _mapper;

        public GetBlogCategoryByIdQueryHandler(IRepository<BlogCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetBlogCategoryByIdQueryResult> Handle(GetBlogCategoryByIdQuery getBlogCategoryByIdQuery)
        {

            DbQueryOptions<BlogCategory> dbQueryOptions = new DbQueryOptions<BlogCategory>();

            Expression<Func<BlogCategory, bool>> filter = (x => x.BlogCategoryID == getBlogCategoryByIdQuery.Id && x.CategoryStatus == true);
            Expression<Func<BlogCategory, object>> shorting = bc => bc.BlogCategoryID;

            dbQueryOptions.filter = filter;
            dbQueryOptions.shorting = shorting;

            BlogCategory value = await _repository.GetByIdAsync(dbQueryOptions);

            GetBlogCategoryByIdQueryResult valueToDto = _mapper.Map<GetBlogCategoryByIdQueryResult>(value);

            return valueToDto;
        }
    }
}

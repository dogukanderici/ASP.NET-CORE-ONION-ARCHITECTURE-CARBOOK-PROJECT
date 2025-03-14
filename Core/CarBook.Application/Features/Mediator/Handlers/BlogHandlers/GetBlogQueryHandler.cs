using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.BlogQueries;
using CarBook.Application.Features.Mediator.Results.BlogResults;
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

namespace CarBook.Application.Features.Mediator.Handlers.BlogHandlers
{
    public class GetBlogQueryHandler : IRequestHandler<GetBlogQuery, List<GetBlogQueryResult>>
    {
        private readonly IRepository<Blog> _repository;
        private readonly IMapper _mapper;

        public GetBlogQueryHandler(IRepository<Blog> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBlogQueryResult>> Handle(GetBlogQuery request, CancellationToken cancellationToken)
        {
            Dictionary<Expression<Func<Blog, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<Blog, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        b=>b.Author,
                        new List<Expression<Func<object, object>>>{}
                    },
                    {
                        b=>b.BlogCategory,
                        new List<Expression<Func<object, object>>>{}
                    },
                    {
                        b=>b.BlogTagClouds,
                        new List<Expression<Func<object, object>>>
                        {
                            btc=>((BlogTagCloud)btc).TagCloud
                        }
                    },
                    {
                        b=>b.BlogComments,
                        new List<Expression<Func<object, object>>>{}
                    }
                };

            DbQueryOptions<Blog> dbQueryOptions = new DbQueryOptions<Blog>();
            dbQueryOptions.thenIncludes = thenIncludes;

            List<Blog> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetBlogQueryResult> valueToDto = _mapper.Map<List<GetBlogQueryResult>>(values);

            return valueToDto;

        }
    }
}

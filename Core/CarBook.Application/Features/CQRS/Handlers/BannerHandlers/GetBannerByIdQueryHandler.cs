using AutoMapper;
using CarBook.Application.Features.CQRS.Queries.BannerQueries;
using CarBook.Application.Features.CQRS.Results.BannerResults;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.BannerHandlers
{
    public class GetBannerByIdQueryHandler
    {
        private readonly IRepository<Banner> _repository;
        private readonly IMapper _mapper;

        public GetBannerByIdQueryHandler(IRepository<Banner> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetBannerByIdQueryResult> Handle(GetBannerByIdQuery getBannerByIdQuery)
        {
            DbQueryOptions<Banner> dbQueryOptions = new DbQueryOptions<Banner>();

            Expression<Func<Banner, bool>> filter = x => x.BannerID == getBannerByIdQuery.Id;

            dbQueryOptions.filter = filter;

            Banner value = await _repository.GetByIdAsync(dbQueryOptions);

            GetBannerByIdQueryResult valueToDto = _mapper.Map<GetBannerByIdQueryResult>(value);

            return valueToDto;
        }
    }
}

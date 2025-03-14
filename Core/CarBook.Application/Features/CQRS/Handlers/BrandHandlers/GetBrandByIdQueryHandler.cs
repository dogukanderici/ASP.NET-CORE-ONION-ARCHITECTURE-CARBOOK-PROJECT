using AutoMapper;
using CarBook.Application.Features.CQRS.Queries.BrandQueries;
using CarBook.Application.Features.CQRS.Results.BrandResults;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.BrandHandlers
{
    public class GetBrandByIdQueryHandler
    {
        private readonly IRepository<Brand> _repository;
        private readonly IMapper _mapper;

        public GetBrandByIdQueryHandler(IRepository<Brand> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetBrandByIdQueryResult> Handle(GetBrandByIdQuery getBrandByIdQuery)
        {
            DbQueryOptions<Brand> dbQueryOptions = new DbQueryOptions<Brand>();

            Expression<Func<Brand, bool>> filter = x => x.BrandID == getBrandByIdQuery.Id;

            dbQueryOptions.filter = filter;

            Brand value = await _repository.GetByIdAsync(dbQueryOptions);

            GetBrandByIdQueryResult valueToDto = _mapper.Map<GetBrandByIdQueryResult>(value);

            return valueToDto;
        }
    }
}

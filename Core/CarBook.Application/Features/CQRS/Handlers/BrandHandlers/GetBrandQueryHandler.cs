using AutoMapper;
using CarBook.Application.Features.CQRS.Results.BrandResults;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.BrandHandlers
{
    public class GetBrandQueryHandler
    {
        private readonly IRepository<Brand> _repository;
        private readonly IMapper _mapper;

        public GetBrandQueryHandler(IRepository<Brand> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBrandQueryResult>> Handle()
        {
            DbQueryOptions<Brand> dbQueryOptions = new DbQueryOptions<Brand>();

            List<Brand> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetBrandQueryResult> valueToDto = _mapper.Map<List<GetBrandQueryResult>>(values);

            return valueToDto;
        }
    }
}

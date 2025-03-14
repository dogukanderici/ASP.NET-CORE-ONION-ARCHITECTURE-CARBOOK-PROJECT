using AutoMapper;
using CarBook.Application.Features.CQRS.Results.BannerResults;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.BannerHandlers
{
    public class GetBannerQueryHandler
    {
        private readonly IRepository<Banner> _repository;
        private readonly IMapper _mapper;

        public GetBannerQueryHandler(IRepository<Banner> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBannerQueryResult>> Handle()
        {
            DbQueryOptions<Banner> dbQueryOptions = new DbQueryOptions<Banner>();

            List<Banner> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetBannerQueryResult> valueToDto = _mapper.Map<List<GetBannerQueryResult>>(values);

            return valueToDto;
        }
    }
}

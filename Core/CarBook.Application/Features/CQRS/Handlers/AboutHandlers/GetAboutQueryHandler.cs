using AutoMapper;
using CarBook.Application.Features.CQRS.Results.AboutResults;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.AboutHandlers
{
    public class GetAboutQueryHandler
    {
        private readonly IRepository<About> _repository;
        private readonly IMapper _mapper;

        public GetAboutQueryHandler(IRepository<About> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAboutQueryResult>> Handle()
        {
            DbQueryOptions<About> dbQueryOptions = new DbQueryOptions<About>();

            List<About> values = await _repository.GetAllAsync(dbQueryOptions);

            return _mapper.Map<List<GetAboutQueryResult>>(values);
        }
    }
}

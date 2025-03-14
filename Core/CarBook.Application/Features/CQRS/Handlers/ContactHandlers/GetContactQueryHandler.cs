using AutoMapper;
using CarBook.Application.Features.CQRS.Results.ContactResults;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.ContactHandlers
{
    public class GetContactQueryHandler
    {
        private readonly IRepository<Contact> _repository;
        private readonly IMapper _mapper;

        public GetContactQueryHandler(IRepository<Contact> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetContactQueryResult>> Handle()
        {
            DbQueryOptions<Contact> dbQueryOptions = new DbQueryOptions<Contact>();

            List<Contact> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetContactQueryResult> valueToDto = _mapper.Map<List<GetContactQueryResult>>(values);

            return valueToDto;
        }
    }
}

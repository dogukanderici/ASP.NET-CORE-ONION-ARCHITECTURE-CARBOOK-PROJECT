using AutoMapper;
using CarBook.Application.Features.CQRS.Queries.ContactQueries;
using CarBook.Application.Features.CQRS.Results.ContactResults;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.ContactHandlers
{
    public class GetContactInboxOutboxQueryHandler
    {
        private readonly IRepository<Contact> _repository;
        private readonly IMapper _mapper;

        public GetContactInboxOutboxQueryHandler(IRepository<Contact> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetContactInboxOutboxQueryResult>> Handle(GetContactByMessageTypeQuery getContactByMessageTypeQuery)
        {
            DbQueryOptions<Contact> dbQueryOptions = new DbQueryOptions<Contact>();

            Expression<Func<Contact, bool>> filter = x => x.MessageType == getContactByMessageTypeQuery.MessageType;
            dbQueryOptions.filter = filter;

            List<Contact> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetContactInboxOutboxQueryResult> valueToDto = _mapper.Map<List<GetContactInboxOutboxQueryResult>>(values);

            return valueToDto;
        }
    }
}

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
    public class GetContactByIdQueryHandler
    {
        private readonly IRepository<Contact> _repository;
        private readonly IMapper _mapper;

        public GetContactByIdQueryHandler(IRepository<Contact> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetContactByIdQueryResult> Hanlde(GetContactByIdQuery getContactByIdQuery)
        {
            DbQueryOptions<Contact> dbQueryOptions = new DbQueryOptions<Contact>();

            Expression<Func<Contact, bool>> filter = x => x.ContactID == getContactByIdQuery.Id;

            dbQueryOptions.filter = filter;

            Contact value = await _repository.GetByIdAsync(dbQueryOptions);

            GetContactByIdQueryResult valueToDto = _mapper.Map<GetContactByIdQueryResult>(value);

            return valueToDto;
        }
    }
}

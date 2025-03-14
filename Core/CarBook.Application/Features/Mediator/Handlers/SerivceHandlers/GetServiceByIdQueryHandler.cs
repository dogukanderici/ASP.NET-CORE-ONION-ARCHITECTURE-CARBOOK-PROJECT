using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.ServiceQueries;
using CarBook.Application.Features.Mediator.Results.ServiceResults;
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

namespace CarBook.Application.Features.Mediator.Handlers.SerivceHandlers
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, GetServiceByIdQueryResult>
    {
        private readonly IRepository<Service> _repository;
        private readonly IMapper _mapper;

        public GetServiceByIdQueryHandler(IRepository<Service> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetServiceByIdQueryResult> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Service> dbQueryOptions = new DbQueryOptions<Service>();

            Expression<Func<Service, bool>> filter = x => x.ServiceID == request.Id;

            dbQueryOptions.filter = filter;

            Service value = await _repository.GetByIdAsync(dbQueryOptions);

            GetServiceByIdQueryResult valueToDto = _mapper.Map<GetServiceByIdQueryResult>(value);

            return valueToDto;
        }
    }
}

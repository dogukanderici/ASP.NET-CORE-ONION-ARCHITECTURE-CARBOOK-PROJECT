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
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.SerivceHandlers
{
    public class GetServiceQueryHandler : IRequestHandler<GetServiceQuery, List<GetServiceQueryResult>>
    {
        private readonly IRepository<Service> _repository;
        private readonly IMapper _mapper;

        public GetServiceQueryHandler(IRepository<Service> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetServiceQueryResult>> Handle(GetServiceQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Service> dbQueryOptions = new DbQueryOptions<Service>();

            List<Service> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetServiceQueryResult> valueToDto = _mapper.Map<List<GetServiceQueryResult>>(values);

            return valueToDto;

        }
    }
}

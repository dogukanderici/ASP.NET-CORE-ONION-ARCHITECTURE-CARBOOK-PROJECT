using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.LocationQueries;
using CarBook.Application.Features.Mediator.Results.LocationResults;
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

namespace CarBook.Application.Features.Mediator.Handlers.LocationHandlers
{
    public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, GetLocationByIdQueryResult>
    {
        private readonly IRepository<Location> _repository;
        private readonly IMapper _mapper;

        public GetLocationByIdQueryHandler(IRepository<Location> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetLocationByIdQueryResult> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Location> dbQueryOptions = new DbQueryOptions<Location>();

            Expression<Func<Location, bool>> filter = x => x.LocationID == request.Id;

            dbQueryOptions.filter = filter;

            Location value = await _repository.GetByIdAsync(dbQueryOptions);

            GetLocationByIdQueryResult valueToDto = _mapper.Map<GetLocationByIdQueryResult>(value);

            return valueToDto;
        }
    }
}

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
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.LocationHandlers
{
    public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, GetLocationDataQueryResult>
    {
        private readonly IRepository<Location> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetLocationQueryHandler(IRepository<Location> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<GetLocationDataQueryResult> Handle(GetLocationQuery request, CancellationToken cancellationToken)
        {

            DbQueryOptions<Location> dbQueryOptions = new DbQueryOptions<Location>();

            if (request.TakeNumber > 0)
            {
                int skipNumber = request.SkipNumber;
                int takeNumber = request.TakeNumber;

                dbQueryOptions.SkipNumber = skipNumber;
                dbQueryOptions.DataTakeNumber = takeNumber;
            }

            dbQueryOptions.shorting = x => x.LocationName;

            int totalDataCount = await _mediator.Send(new GetLocationCountQuery());

            List<Location> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetLocationQueryResult> valueToDto = _mapper.Map<List<GetLocationQueryResult>>(values);

            GetLocationDataQueryResult data = new GetLocationDataQueryResult();

            data.Locations = valueToDto;
            data.LocationCount = totalDataCount;

            return data;
        }
    }
}

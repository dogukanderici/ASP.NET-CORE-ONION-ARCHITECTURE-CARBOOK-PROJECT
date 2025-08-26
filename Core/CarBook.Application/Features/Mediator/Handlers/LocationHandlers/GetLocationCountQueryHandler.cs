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
    public class GetLocationCountQueryHandler : IRequestHandler<GetLocationCountQuery, int>
    {
        private readonly IRepository<Location> _repository;

        public GetLocationCountQueryHandler(IRepository<Location> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetLocationCountQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Location> dbQueryOptions = new DbQueryOptions<Location>();

            int totalDataCount = await _repository.GetDataCount(dbQueryOptions);

            return totalDataCount;
        }
    }
}

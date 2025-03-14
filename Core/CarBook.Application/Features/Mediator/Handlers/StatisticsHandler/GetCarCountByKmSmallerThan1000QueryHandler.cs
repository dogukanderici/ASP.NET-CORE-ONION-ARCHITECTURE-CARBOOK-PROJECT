using CarBook.Application.Features.Mediator.Queries.StatisticsQueries;
using CarBook.Application.Features.Mediator.Results.StatisticsResults;
using CarBook.Application.Interfaces.StatisticsInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.StatisticsHandler
{
    public class GetCarCountByKmSmallerThan1000QueryHandler : IRequestHandler<GetCarCountByKmSmallerThan1000Query, GetCarCountByKmSmallerThan1000QueryResult>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetCarCountByKmSmallerThan1000QueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<GetCarCountByKmSmallerThan1000QueryResult> Handle(GetCarCountByKmSmallerThan1000Query request, CancellationToken cancellationToken)
        {
            var value = _statisticsRepository.GetCarCountByKmSmallerThan1000();

            GetCarCountByKmSmallerThan1000QueryResult getCarCountByKmSmallerThan1000Result = new GetCarCountByKmSmallerThan1000QueryResult
            {
                CarCountByKmSmallerThan1000 = value
            };

            return getCarCountByKmSmallerThan1000Result;
        }
    }
}

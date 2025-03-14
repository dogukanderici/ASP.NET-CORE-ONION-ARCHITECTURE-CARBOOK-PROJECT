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
    public class GetAvgRentPriceForHourlyCountQueryHandler : IRequestHandler<GetAvgRentPriceForHourlyCountQuery, GetAvgRentPriceForHourlyCountQueryResult>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetAvgRentPriceForHourlyCountQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<GetAvgRentPriceForHourlyCountQueryResult> Handle(GetAvgRentPriceForHourlyCountQuery request, CancellationToken cancellationToken)
        {
            var value = _statisticsRepository.GetAvgRentPriceForHourlyCount();

            GetAvgRentPriceForHourlyCountQueryResult getAvgRentPriceForHourlyCountQueryResult = new GetAvgRentPriceForHourlyCountQueryResult
            {
                AvgRentPriceForHourlyCount = value
            };

            return getAvgRentPriceForHourlyCountQueryResult;
        }
    }
}

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
    public class GetAvgRentPriceForWeeklyCountQueryHandler : IRequestHandler<GetAvgRentPriceForWeeklyCountQuery, GetAvgRentPriceForWeeklyCountQueryResult>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetAvgRentPriceForWeeklyCountQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<GetAvgRentPriceForWeeklyCountQueryResult> Handle(GetAvgRentPriceForWeeklyCountQuery request, CancellationToken cancellationToken)
        {
            var value = _statisticsRepository.GetAvgRentPriceForWeeklyCount();

            GetAvgRentPriceForWeeklyCountQueryResult getAvgRentPriceForWeeklyCountQueryResult = new GetAvgRentPriceForWeeklyCountQueryResult
            {
                AvgRentPriceForWeeklyCount = value
            };

            return getAvgRentPriceForWeeklyCountQueryResult;
        }
    }
}

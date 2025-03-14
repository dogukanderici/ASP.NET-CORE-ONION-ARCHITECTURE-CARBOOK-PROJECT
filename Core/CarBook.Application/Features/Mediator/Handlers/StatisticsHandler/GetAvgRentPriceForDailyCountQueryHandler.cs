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
    public class GetAvgRentPriceForDailyCountQueryHandler : IRequestHandler<GetAvgRentPriceForDailyCountQuery, GetAvgRentPriceForDailyCountQueryResult>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetAvgRentPriceForDailyCountQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<GetAvgRentPriceForDailyCountQueryResult> Handle(GetAvgRentPriceForDailyCountQuery request, CancellationToken cancellationToken)
        {
            var value = _statisticsRepository.GetAvgRentPriceForDailyCount();

            GetAvgRentPriceForDailyCountQueryResult getAvgRentPriceForDailyCountQueryResult = new GetAvgRentPriceForDailyCountQueryResult
            {
                AvgRentPriceForDailyCount = value
            };

            return getAvgRentPriceForDailyCountQueryResult;
        }
    }
}

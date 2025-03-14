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
    public class GetAvgRentPriceForMountlyCountQueryHandler : IRequestHandler<GetAvgRentPriceForMountlyCountQuery, GetAvgRentPriceForMountlyCountQueryResult>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetAvgRentPriceForMountlyCountQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<GetAvgRentPriceForMountlyCountQueryResult> Handle(GetAvgRentPriceForMountlyCountQuery request, CancellationToken cancellationToken)
        {
            var value = _statisticsRepository.GetAvgRentPriceForMountlyCount();

            GetAvgRentPriceForMountlyCountQueryResult getAvgRentPriceForMountlyCountQueryResult = new GetAvgRentPriceForMountlyCountQueryResult
            {
                AvgRentPriceForMountlyCount = value
            };

            return getAvgRentPriceForMountlyCountQueryResult;
        }
    }
}

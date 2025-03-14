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
    public class GetCarBrandAndModelByPriceDailyMaxQueryHandler : IRequestHandler<GetCarBrandAndModelByPriceDailyMaxQuery, GetCarBrandAndModelByPriceDailyMaxQueryResult>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetCarBrandAndModelByPriceDailyMaxQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<GetCarBrandAndModelByPriceDailyMaxQueryResult> Handle(GetCarBrandAndModelByPriceDailyMaxQuery request, CancellationToken cancellationToken)
        {
            var value = _statisticsRepository.GetCarBrandAndModelByPriceDailyMax();

            GetCarBrandAndModelByPriceDailyMaxQueryResult getCarBrandAndModelByPriceDailyMaxResult = new GetCarBrandAndModelByPriceDailyMaxQueryResult
            {
                CarBrandAndModelByPriceDailyMax = value
            };

            return getCarBrandAndModelByPriceDailyMaxResult;
        }
    }
}

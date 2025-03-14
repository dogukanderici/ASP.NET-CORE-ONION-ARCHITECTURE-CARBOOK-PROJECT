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
    public class GetCarCountByFuelGasOrDieselQueryHandler : IRequestHandler<GetCarCountByFuelGasOrDieselQuery, GetCarCountByFuelGasOrDieselQueryResult>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetCarCountByFuelGasOrDieselQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<GetCarCountByFuelGasOrDieselQueryResult> Handle(GetCarCountByFuelGasOrDieselQuery request, CancellationToken cancellationToken)
        {
            var value = _statisticsRepository.GetCarCountByFuelGasOrDiesel();

            GetCarCountByFuelGasOrDieselQueryResult getCarCountByFuelGasOrDieselResult = new GetCarCountByFuelGasOrDieselQueryResult
            {
                CarCountByFuelGasOrDiesel = value
            };

            return getCarCountByFuelGasOrDieselResult;
        }
    }
}

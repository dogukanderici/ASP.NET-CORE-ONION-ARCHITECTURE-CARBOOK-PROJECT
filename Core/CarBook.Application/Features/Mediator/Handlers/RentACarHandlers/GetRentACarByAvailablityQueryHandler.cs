using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.RentACarQueries;
using CarBook.Application.Features.Mediator.Results.RentACarResults;
using CarBook.Application.Interfaces;
using CarBook.Application.Interfaces.RentACarInterfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.RentACarHandlers
{
    public class GetRentACarByAvailablityQueryHandler : IRequestHandler<GetRentACarByAvailablityQuery, List<GetRentACarByAvailablityQueryResult>>
    {
        private readonly IRepository<RentACar> _repository;
        private readonly IRentACarRepository _repository2;
        private readonly IMapper _mapper;

        public GetRentACarByAvailablityQueryHandler(IRepository<RentACar> repository, IMapper mapper, IRentACarRepository repository2)
        {
            _repository = repository;
            _mapper = mapper;
            _repository2 = repository2;
        }

        public async Task<List<GetRentACarByAvailablityQueryResult>> Handle(GetRentACarByAvailablityQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<RentACar> dbQueryOptions = new DbQueryOptions<RentACar>();

            Expression<Func<RentACar, bool>> filter = (x => x.LocationID == request.LocationID &&
            x.Available == true &&
            (
            ((x.PickUpDate <= request.PickUpDate) && (x.DropOffDate > request.PickUpDate)) &&
            ((x.PickUpDate < request.DropOffDate) && (x.DropOffDate > request.DropOffDate))));

            Dictionary<Expression<Func<RentACar, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<RentACar, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        x=>x.Car,
                        new List<Expression<Func<object, object>>>
                        {
                            y=>((Car)y).Brand
                        }
                    },
                    {
                        x=>x.Car,
                        new List<Expression<Func<object, object>>>
                        {
                            y=>((Car)y).CarPricings,
                            z=>((CarPricing)z).PricingType
                        }
                    },
                    {
                        x=>x.Location,
                        new List<Expression<Func<object, object>>>{}
                    }
                };

            dbQueryOptions.filter = filter;
            dbQueryOptions.thenIncludes = thenIncludes;

            //List<RentACar> values = _repository2.GetDataWithInnerFilter(request);
            List<RentACar> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetRentACarByAvailablityQueryResult> valueToDto = _mapper.Map<List<GetRentACarByAvailablityQueryResult>>(values);

            return valueToDto;
        }
    }
}

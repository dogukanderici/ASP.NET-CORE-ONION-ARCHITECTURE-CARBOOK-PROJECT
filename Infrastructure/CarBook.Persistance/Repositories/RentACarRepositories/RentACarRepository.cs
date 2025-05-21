using Azure.Core;
using CarBook.Application.Features.Mediator.Queries.RentACarQueries;
using CarBook.Application.Interfaces.RentACarInterfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using CarBook.Persistance.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Persistance.Repositories.RentACarRepositories
{
    public class RentACarRepository : IRentACarRepository
    {
        private readonly CarBookContext _context;

        public RentACarRepository(CarBookContext context)
        {
            _context = context;
        }

        public List<RentACar> GetDataWithInnerFilter(GetRentACarByAvailablityQuery request)
        {
            var result = _context.RentACars
                .Where(r => r.LocationID == request.LocationID && r.Available == request.Available &&
                ((request.DropOffDate < r.PickUpDate) || (request.PickUpDate > r.DropOffDate))
                && !_context.RentACars.Where(inner => inner.LocationID != request.LocationID && inner.DropOffDate < request.DropOffDate)
                    .Select(inner => inner.CarID)
                    .Contains(r.CarID))
                .Include(x=>x.Car)
                .ThenInclude(z=>z.CarPricings)
                .ThenInclude(k=>k.PricingType)
                .Include(x => x.Car)
                .ThenInclude(y=>y.Brand)
                .Include(x=>x.Location)
                .ToList();

            return result;
        }
    }
}

using CarBook.Application.Features.Mediator.Queries.RentACarQueries;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Interfaces.RentACarInterfaces
{
    public interface IRentACarRepository
    {
        List<RentACar> GetDataWithInnerFilter(GetRentACarByAvailablityQuery request);
    }
}

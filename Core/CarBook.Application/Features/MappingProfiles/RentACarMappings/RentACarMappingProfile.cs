using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.RentACarCommands;
using CarBook.Application.Features.Mediator.Results.RentACarResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.MappingProfiles.RentACarMappings
{
    public class RentACarMappingProfile : Profile
    {
        public RentACarMappingProfile()
        {
            CreateMap<RentACar, GetRentACarQueryResult>().ReverseMap();
            CreateMap<RentACar, GetRentACarForCarQueryResult>().ReverseMap();
            CreateMap<RentACar, GetRentACarForReservationQueryResult>().ReverseMap();
            CreateMap<RentACar, GetRentACarByIdQueryResult>().ReverseMap();
            CreateMap<RentACar, GetRentACarByCarIdQueryResult>().ReverseMap();
            CreateMap<RentACar, GetRentACarByAvailablityQueryResult>().ReverseMap();
            CreateMap<RentACar, CreateRentACarCommand>().ReverseMap();
            CreateMap<RentACar, UpdateRentACarCommand>().ReverseMap();
        }
    }
}

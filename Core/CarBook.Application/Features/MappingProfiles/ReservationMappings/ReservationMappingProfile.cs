using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.ReservationCommands;
using CarBook.Application.Features.Mediator.Results.ReservationResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.MappingProfiles.ReservationMappings
{
    public class ReservationMappingProfile : Profile
    {
        public ReservationMappingProfile()
        {
            CreateMap<Reservation, GetReservationQueryResult>().ReverseMap();
            CreateMap<Reservation, GetReservationByIdQueryResult>().ReverseMap();
            CreateMap<Reservation, GetReservationForRentACarQueryResult>().ReverseMap();
            CreateMap<Reservation, CreateReservationCommand>().ReverseMap();
            CreateMap<Reservation, UpdateReservationCommand>().ReverseMap();
        }
    }
}

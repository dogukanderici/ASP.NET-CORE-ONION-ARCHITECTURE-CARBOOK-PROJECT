using AutoMapper;
using CarBook.Application.Features.CQRS.Commands.CarCommands;
using CarBook.Application.Features.CQRS.Results.CarResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.MappingProfiles.CarMappings
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<Car, GetCarQueryResult>().ReverseMap();
            CreateMap<Car, GetCarForCarFeatureQueryResult>().ReverseMap();
            CreateMap<Car, GetCarForOnlyCarPricingQueryResult>().ReverseMap();
            CreateMap<Car, GetCarForRentACarQueryResult>().ReverseMap();
            CreateMap<Car, GetLast5CarsQueryResult>().ReverseMap();
            CreateMap<Car, GetCarByIdQueryResult>().ReverseMap();
            CreateMap<Car, GetCarByIdForCarReviewQueryResult>().ReverseMap();
            CreateMap<Car, CreateCarCommand>().ReverseMap();
            CreateMap<Car, UpdateCarCommand>().ReverseMap();
        }
    }
}

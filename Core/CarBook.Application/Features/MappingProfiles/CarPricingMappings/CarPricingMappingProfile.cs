using AutoMapper;
using CarBook.Application.Features.CQRS.Commands.CarPricingCommands;
using CarBook.Application.Features.CQRS.Results.CarPricingResult;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.MappingProfiles.CarPricingMappings
{
    public class CarPricingMappingProfile : Profile
    {
        public CarPricingMappingProfile()
        {
            CreateMap<CarPricing, GetCarPricingQueryResult>().ReverseMap();
            CreateMap<CarPricing, GetCarPricingForCarQueryResult>().ReverseMap();
            CreateMap<CarPricing, GetCarPricingByIdQueryResult>().ReverseMap();
            CreateMap<CarPricing, CreateCarPricingCommand>().ReverseMap();
            CreateMap<CarPricing, UpdateCarPricingCommand>().ReverseMap();
        }
    }
}

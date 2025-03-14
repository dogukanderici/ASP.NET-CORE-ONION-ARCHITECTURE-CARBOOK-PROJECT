using AutoMapper;
using CarBook.Application.Features.CQRS.Commands.CarFeatureCommands;
using CarBook.Application.Features.CQRS.Results.CarFeatureResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.MappingProfiles.CarFeatureMappings
{
    public class CarFeatureMappingProfile : Profile
    {
        public CarFeatureMappingProfile()
        {
            CreateMap<CarFeature, GetCarFeatureQueryResult>().ReverseMap();
            CreateMap<CarFeature, GetCarFeatureForCarQueryResult>().ReverseMap();
            CreateMap<CarFeature, GetCarFeatureByIdQueryResult>().ReverseMap();
            CreateMap<CarFeature, CreateCarFeatureCommand>().ReverseMap();
            CreateMap<CarFeature, UpdateCarFeatureCommand>().ReverseMap();
        }
    }
}

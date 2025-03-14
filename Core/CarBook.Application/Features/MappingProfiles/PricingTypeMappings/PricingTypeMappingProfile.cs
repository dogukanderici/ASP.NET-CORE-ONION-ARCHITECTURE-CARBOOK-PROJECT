using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.PricingTypeCommands;
using CarBook.Application.Features.Mediator.Handlers.PricingTypeHandlers;
using CarBook.Application.Features.Mediator.Results.PricingTypeResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.MappingProfiles.PricingTypeMappings
{
    public class PricingTypeMappingProfile : Profile
    {
        public PricingTypeMappingProfile()
        {
            CreateMap<PricingType, GetPricingTypeQueryResult>().ReverseMap();
            CreateMap<PricingType, GetPricingTypeByIdQueryResult>().ReverseMap();
            CreateMap<PricingType, CreatePricingTypeCommand>().ReverseMap();
            CreateMap<PricingType, UpdatePricingTypeCommand>().ReverseMap();
        }
    }
}

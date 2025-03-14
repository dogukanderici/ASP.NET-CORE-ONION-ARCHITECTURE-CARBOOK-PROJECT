using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.FooterAddressCommands;
using CarBook.Application.Features.Mediator.Results.FooterAddressResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.MappingProfiles.FooterAddressMappings
{
    public class FooterAddressMappingProfile : Profile
    {
        public FooterAddressMappingProfile()
        {
            CreateMap<FooterAddress, GetFooterAddressQueryResult>().ReverseMap();
            CreateMap<FooterAddress, GetFooterAddressByIdQueryResult>().ReverseMap();
            CreateMap<FooterAddress, CreateFooterAddressCommand>().ReverseMap();
            CreateMap<FooterAddress, UpdateFooterAddressCommand>().ReverseMap();
        }
    }
}

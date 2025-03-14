using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.TagCloudCommands;
using CarBook.Application.Features.Mediator.Results.TagCloudResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.MappingProfiles.TagCloudMappings
{
    public class TagCloudMappingProfile : Profile
    {
        public TagCloudMappingProfile()
        {
            CreateMap<TagCloud, GetTagCloudQueryResult>().ReverseMap();
            CreateMap<TagCloud, GetTagCloudByIdQueryResult>().ReverseMap();
            CreateMap<TagCloud, CreateTagCloudCommand>().ReverseMap();
            CreateMap<TagCloud, UpdateTagCloudCommand>().ReverseMap();
        }
    }
}

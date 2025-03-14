using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.BlogTagCloudCommands;
using CarBook.Application.Features.Mediator.Results.BlogTagCloudResult;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.MappingProfiles.BlogTagCloudMappings
{
    public class BlogTagMappingProfile : Profile
    {
        public BlogTagMappingProfile()
        {
            CreateMap<BlogTagCloud, GetBlogTagCloudQueryResult>().ReverseMap();
            CreateMap<BlogTagCloud, GetBlogTagCloudForBlogQueryResult>().ReverseMap();
            CreateMap<BlogTagCloud, GetBlogTagCloudByIdQueryResult>().ReverseMap();
            CreateMap<BlogTagCloud, CreateBlogTagCloudCommand>().ReverseMap();
            CreateMap<BlogTagCloud, UpdateBlogTagCloudCommand>().ReverseMap();
        }
    }
}

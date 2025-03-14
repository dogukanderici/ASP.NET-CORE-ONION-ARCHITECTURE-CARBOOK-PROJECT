using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.BlogCommands;
using CarBook.Application.Features.Mediator.Results.BlogResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.MappingProfiles.BlogMappings
{
    public class BlogMappingProfile : Profile
    {
        public BlogMappingProfile()
        {
            CreateMap<Blog, GetBlogQueryResult>().ReverseMap();
            CreateMap<Blog, GetLast3BlogQueryResult>().ReverseMap();
            CreateMap<Blog, GetBlogWithCommentQueryResult>().ReverseMap();
            CreateMap<Blog, GetBlogWithPublishStateQueryResult>().ReverseMap();
            CreateMap<Blog, GetBlogByIdQueryResult>().ReverseMap();
            CreateMap<Blog, CreateBlogCommand>().ReverseMap();
            CreateMap<Blog, UpdateBlogCommand>().ReverseMap();
        }
    }
}

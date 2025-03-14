using AutoMapper;
using CarBook.Application.Features.CQRS.Commands.BlogCategoryCommands;
using CarBook.Application.Features.CQRS.Results.BlogCategoryResults;
using CarBook.Application.Features.CQRS.Results.CategoryResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.MappingProfiles.BlogCategoryMappings
{
    public class BlogCategoryMappingProfile : Profile
    {
        public BlogCategoryMappingProfile()
        {
            CreateMap<BlogCategory, GetBlogCategoryQueryResult>().ReverseMap();
            CreateMap<BlogCategory, GetBlogCategoryByIdQueryResult>().ReverseMap();
            CreateMap<BlogCategory, CreateBlogCategoryCommand>().ReverseMap();
            CreateMap<BlogCategory, UpdateBlogCategoryCommand>().ReverseMap();
        }
    }
}

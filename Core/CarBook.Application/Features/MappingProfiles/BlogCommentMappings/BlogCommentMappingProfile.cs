using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.BlogCommentCommands;
using CarBook.Application.Features.Mediator.Results.BlogCommentResult;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.MappingProfiles.BlogCommentMappings
{
    public class BlogCommentMappingProfile : Profile
    {
        public BlogCommentMappingProfile()
        {
            CreateMap<BlogComment, GetBlogCommentQueryResult>().ReverseMap();
            CreateMap<BlogComment, GetBlogCommentForBlogQueryResult>().ReverseMap();
            CreateMap<BlogComment, GetBlogCommentByIdQueryResult>().ReverseMap();
            CreateMap<BlogComment, GetBlogCommentByBlogIdQueryResult>().ReverseMap();
            CreateMap<BlogComment, CreateBlogCommentCommand>().ReverseMap();
            CreateMap<BlogComment, UpdateBlogCommentCommand>().ReverseMap();
        }
    }
}

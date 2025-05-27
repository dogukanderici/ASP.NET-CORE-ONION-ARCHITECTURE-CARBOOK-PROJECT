using AutoMapper;
using CarBook.Application.Features.CQRS.Commands.CarReviewCommands;
using CarBook.Application.Features.CQRS.Results.CarReviewResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.MappingProfiles.CarReviewMappings
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<CarReview, GetCarReviewQueryResult>().ReverseMap();
            CreateMap<CarReview, GetCarReviewByIdQueryResult>().ReverseMap();
            CreateMap<CarReview, GetCarReviewByCarIdQueryResult>().ReverseMap();
            CreateMap<CarReview, CreateCarReviewCommand>().ReverseMap();
            CreateMap<CarReview, UpdateCarReviewCommand>().ReverseMap();
        }
    }
}

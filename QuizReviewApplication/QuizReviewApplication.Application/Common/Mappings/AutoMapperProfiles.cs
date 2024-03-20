using AutoMapper;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Common.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Question, QuestionDto>()
                .ForMember(dest => dest.SkillLevel, opt => opt.MapFrom(src => (int)src.SkillLevel))
                .ForMember(dest => dest.QuestionLevel, opt => opt.MapFrom(src => (int)src.QuestionLevel));
        }
    }
}

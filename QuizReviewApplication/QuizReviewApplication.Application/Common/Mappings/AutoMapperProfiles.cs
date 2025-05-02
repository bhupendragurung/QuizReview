using AutoMapper;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.QuestionAnswers.Commands.CreateQuestionAnswer;
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
            CreateMap<Answer, AnswerDto>()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question.Text))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Marks));
            CreateMap<Question, QuestionDto>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.SkillLevel, opt => opt.MapFrom(src => (int)src.SkillLevel))
                .ForMember(dest => dest.QuestionLevel, opt => opt.MapFrom(src => (int)src.QuestionLevel));
            // .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.QuestionCategories.FirstOrDefault());

            CreateMap<RegisterDto, ApplicationUser>();
            CreateMap<CreateQuestionAnswerCommand, Answer>();
        }
    }
}

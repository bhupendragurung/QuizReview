using AutoMapper;
using FluentAssertions;
using QuizReviewApplication.Application.Common.Mappings;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.UnitTests.Features.AutoMapper
{
    public class AutoMapperTest
    {
        private readonly IMapper _mapper;
        public AutoMapperTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Should_MatchProperly_QuestionToQuestionDto()
        {
            //Arrange
            var question = new Question
            {
                Id = Guid.NewGuid(),
                Text = "What is c#",
                QuestionLevel = Domain.Enum.QuestionType.Easy,
                SkillLevel = Domain.Enum.SkillType.EntryLevel

            };
            //Act
            var questionDto=_mapper.Map<QuestionDto>(question);

            //Assert
            questionDto.Id.Should().Be(question.Id);
            questionDto.Content.Should().Be(question.Text);
            questionDto.SkillLevel.Should().Be((int)question.SkillLevel);
            questionDto.QuestionLevel.Should().Be((int)question.QuestionLevel);


        }
    }
}

using AutoMapper;
using FluentAssertions;
using Moq;
using QuizReviewApplication.Application.Common.Mappings;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.Questions.Queries.GetQuestions;
using QuizReviewApplication.Application.Repositories;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.UnitTests.Features.Questions.Queries
{
    public class GetQuestionsHandlerTests
    {
        private readonly Mock<IQuestionRepository> _questionRepositoryMock;
        private readonly IMapper _mapperMock;

       // public GetQuestionsHandlerTests()
       // {
       //     _questionRepositoryMock = new();
       //     var mappingConfig = new MapperConfiguration(mc =>
       //     {
       //         mc.AddProfile(new AutoMapperProfiles());
       //     });
       //     _mapperMock = mappingConfig.CreateMapper();

       // }
       // [Fact]
       // public async Task Handle_Should_ReturnQuestionList_WhenDataFound()
       // {
       //     // Arrange
       //     var questionResponse = new GetQuestionsResponse();
       //     List<Question> questions = new List<Question>()
       //     {

       //         new Question
       //         {   Id=Guid.NewGuid(),
       //             QuestionLevel=Domain.Enum.QuestionType.Medium,
       //             SkillLevel=Domain.Enum.SkillType.Intermediate,
       //             Text="First"
       //         }, new Question
       //         {   Id=Guid.NewGuid(),
       //             QuestionLevel=Domain.Enum.QuestionType.Easy,
       //             SkillLevel=Domain.Enum.SkillType.EntryLevel,
       //             Text="Second"
       //         }
       //     };
           
       //     var query = new GetQuestionsQuery();
       //     _questionRepositoryMock.Setup(q => q.GetAllQuestionsAsync()).ReturnsAsync(questions);
       //     var handler = new GetQuestionsHandler(_questionRepositoryMock.Object, _mapperMock);

       //     //Act 
       //     var result = await handler.Handle(query, default);

       //     // Assert
       //     result.Success.Should().BeTrue();
       //     result.Questions.Should().NotBeEmpty().And.NotBeNull();
       //     result.Questions.Should().HaveCount(questions.Count);
       //     result.Questions.Should().SatisfyRespectively(
       //first =>
       //{
       //    first.Id.Should().Be(questions[0].Id);
       //    first.Content.Should().Be("First");
       //    first.SkillLevel.Should().Be(2);
       //    first.QuestionLevel.Should().Be(2);
       //}
       //, second =>
       //{
       //    second.Id.Should().Be(questions[1].Id);
       //    second.Content.Should().Be("Second");
       //    second.SkillLevel.Should().Be(1);
       //    second.QuestionLevel.Should().Be(1);
       //}
       //);
       // }
       //     [Fact]
       // public async Task Handle_Should_ReturnEmptyQuestionList_NoDataFound()
       // {
       //     // Arrange
       //     List<Question> questions = new List<Question>()
       //     {

       //     };
       //     var query = new GetQuestionsQuery();
       //     _questionRepositoryMock.Setup(q => q.GetAllQuestionsAsync()).ReturnsAsync(questions);
       //     var handler = new GetQuestionsHandler(_questionRepositoryMock.Object, _mapperMock);

       //     //Act 
       //     var result = await handler.Handle(query, default);

       //     //Assert
       //     result.Success.Should().BeTrue();
       //     result.Questions.Should().BeEmpty();
       //     result.Questions.Should().HaveCount(questions.Count);
       // }

    }
}

using AutoMapper;
using FluentAssertions;
using Moq;
using QuizReviewApplication.Application.Questions.Queries.GetQuestions;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.UnitTests.Questions.Queries
{
    public class GetQuestionsHandlerTests
    {
        private readonly Mock<IQuestionRepository> _questionRepositoryMock; 
        private readonly Mock<IMapper> _mapperMock;

        public GetQuestionsHandlerTests()
        {
                _questionRepositoryMock = new();
                _mapperMock = new();

        }
        [Fact]
        public async Task Handle_Should_ReturnQuestionList_WhenDataFound()
        {
            // Arrange
            List<Question> questions = new List<Question>()
            {

                new Question
                {   Id=Guid.NewGuid(),
                    QuestionLevel=Domain.Enum.QuestionType.Medium,
                    SkillLevel=Domain.Enum.SkillType.MidLevel,
                    Text="First"
                },
                 new Question
                {   Id=Guid.NewGuid(),
                    QuestionLevel=Domain.Enum.QuestionType.Medium,
                    SkillLevel=Domain.Enum.SkillType.MidLevel,
                    Text="First"
                }
            };
            var query= new GetQuestionsQuery();
          //  _questionRepositoryMock.Setup(q => q.GetAllQuestionsAsync()).ReturnsAsync(questions);
            var handler=new GetQuestionsHandler(_questionRepositoryMock.Object,_mapperMock.Object);

            //Act 
             var result= await handler.Handle(query, default);

            //Assert
            result.Should().NotBeEmpty().And.NotBeNull();
            result.Should().HaveCount(questions.Count);
            // check data also equal 
        }

        [Fact]
        public async Task Handle_Should_ReturnEmptyQuestionList_NoDataFound()
        {
            // Arrange
            List<Question> questions = new List<Question>()
            {
              
            };
            var query = new GetQuestionsQuery();
            //_questionRepositoryMock.Setup(q => q.GetAllQuestionsAsync()).ReturnsAsync(questions);
            var handler = new GetQuestionsHandler(_questionRepositoryMock.Object, _mapperMock.Object);

            //Act 
            var result = await handler.Handle(query, default);

            //Assert
            result.Should().BeEmpty();
        }

    }
}

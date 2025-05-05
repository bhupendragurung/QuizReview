using AutoMapper;
using FluentAssertions;
using Moq;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.QuestionAnswers.Queries.GetQuestionAnswer;
using QuizReviewApplication.Application.Repositories;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.UnitTests.Features.QuestionAnswers.Handlers
{
    public class GetAnswerHandlerTests
    {
        private readonly Mock<IAnswerRepository> _answerRepoMock;
        private readonly IMapper _mapper;
        private readonly GetAnswerHandler _handler;

        public GetAnswerHandlerTests()
        {
            _answerRepoMock = new Mock<IAnswerRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Answer, AnswerDto>();
            });
            _mapper = config.CreateMapper();

            _handler = new GetAnswerHandler(_answerRepoMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnAnswers_WhenRepositoryReturnsData()
        {
            // Arrange
            var answers = new List<Answer>
        {
            new Answer { Id = Guid.NewGuid(), Text = "Answer 1", Marks = 2 },
            new Answer { Id = Guid.NewGuid(), Text = "Answer 2", Marks = 3 }
        };

            _answerRepoMock.Setup(r => r.GetAllQuestionAnswerAsync())
                           .ReturnsAsync(answers);

            var query = new GetAnswerQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Data.Should().HaveCount(2);
            result.Message.Should().Be("All Question Fetehced Successfully");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            _answerRepoMock.Setup(r => r.GetAllQuestionAnswerAsync())
                           .ThrowsAsync(new Exception("DB failure"));

            var query = new GetAnswerQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Failed to retrieve answers.");
        }
    }
}

using AutoMapper;
using FluentAssertions;
using Moq;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.Questions.Queries.GetQuestions;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.UnitTests.Features.Questions.Handlers
{
    public class GetQuestionsHandlerTests
    {
        private readonly Mock<IQuestionRepository> _questionRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetQuestionsHandler _handler;

        public GetQuestionsHandlerTests()
        {
            _questionRepositoryMock = new Mock<IQuestionRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetQuestionsHandler(_questionRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenQuestionsRetrieved()
        {
            // Arrange
            var query = new GetQuestionsQuery
            {
                PageNumber = 1,
                PageSize = 10,
                Search = "",
                SkillLevel = null,
                QuestionLevel = null
            };

            var samplePagedList = new PagedList<QuestionDto>(
                new List<QuestionDto>
                {
                new QuestionDto { Id = Guid.NewGuid(), Content = "What is AI?" },
                new QuestionDto { Id = Guid.NewGuid(), Content = "Define Machine Learning." }
                },
                count: 2,
                pageNumber: 1,
                pageSize: 10
            );

            _questionRepositoryMock
                .Setup(repo => repo.GetAllQuestionsAsync(query))
                .ReturnsAsync(samplePagedList);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.TotalCount.Should().Be(2);
            result.Message.Should().Be("All Question Fetehced Successfully");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenExceptionThrown()
        {
            // Arrange
            var query = new GetQuestionsQuery { PageNumber = 1, PageSize = 10 };

            _questionRepositoryMock
                .Setup(repo => repo.GetAllQuestionsAsync(query))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Failed to retrieve questions.");
            result.Data.Should().BeNull();
        }
    }
}

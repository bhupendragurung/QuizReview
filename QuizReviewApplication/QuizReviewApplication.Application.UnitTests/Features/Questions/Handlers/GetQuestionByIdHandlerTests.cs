using AutoMapper;
using FluentAssertions;
using Moq;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.Questions.Queries.GetQuestionById;
using QuizReviewApplication.Application.Repositories;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.UnitTests.Features.Questions.Handlers
{

    public class GetQuestionByIdHandlerTests
    {
        private readonly Mock<IQuestionRepository> _questionRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetQuestionByIdHandler _handler;

        public GetQuestionByIdHandlerTests()
        {
            _questionRepositoryMock = new Mock<IQuestionRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetQuestionByIdHandler(_questionRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenQuestionExists()
        {
            // Arrange
            var questionId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var question = new Question
            {
                Id = questionId,
                Text = "What is AI?",
                SkillLevel = SkillType.Intermediate,
                QuestionLevel = QuestionType.Medium,
                QuestionCategories = new List<QuestionCategory>
            {
                new QuestionCategory { CategoryId = categoryId }
            }
            };

            var questionDto = new QuestionDto
            {
                Id = questionId,
                Content = question.Text,
                SkillLevel = (int)( SkillType.Intermediate),
                QuestionLevel =(int) QuestionType.Medium
            };

            _questionRepositoryMock.Setup(repo => repo.GetQuestionByIdAsync(questionId))
                .ReturnsAsync(question);

            _mapperMock.Setup(mapper => mapper.Map<QuestionDto>(question))
                .Returns(questionDto);

            var query = new GetQuestionByIdQuery { QuestionId = questionId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Id.Should().Be(questionId);
            result.Data.CategoryId.Should().Be(categoryId);
            result.Message.Should().Be("Question Feteched Successfully");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenQuestionNotFound()
        {
            // Arrange
            var questionId = Guid.NewGuid();

            _questionRepositoryMock.Setup(repo => repo.GetQuestionByIdAsync(questionId))
                .ReturnsAsync((Question)null);

            var query = new GetQuestionByIdQuery { QuestionId = questionId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Question not found.");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenExceptionThrown()
        {
            // Arrange
            var questionId = Guid.NewGuid();

            _questionRepositoryMock.Setup(repo => repo.GetQuestionByIdAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Database error"));

            var query = new GetQuestionByIdQuery { QuestionId = questionId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("An error occurred while fetching the question.");
        }
    }
}

using FluentAssertions;
using Moq;
using QuizReviewApplication.Application.Features.Questions.Commands.CreateQuestion;
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
    public class CreateQuestionHandlerTests
    {
        private readonly Mock<IQuestionRepository> _questionRepoMock;
        private readonly Mock<ICategoryRepository> _categoryRepoMock;
        private readonly Mock<IQuestionCategoryRepository> _questionCategoryRepoMock;
        private readonly CreateQuestionHandler _handler;

        public CreateQuestionHandlerTests()
        {
            _questionRepoMock = new Mock<IQuestionRepository>();
            _categoryRepoMock = new Mock<ICategoryRepository>();
            _questionCategoryRepoMock = new Mock<IQuestionCategoryRepository>();

            _handler = new CreateQuestionHandler(
                _questionRepoMock.Object,
                _categoryRepoMock.Object,
                _questionCategoryRepoMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenQuestionIsCreatedAndCategoryLinked()
        {
            // Arrange
            var command = new CreateQuestionCommand
            {
                Content = "What is AI?",
                SkillLevel = (int)SkillType.EntryLevel,
                QuestionLevel = (int)QuestionType.Easy,
                CategoryId = Guid.NewGuid()
            };

            _questionRepoMock.Setup(x => x.GetQuestionByContentAsync(command.Content))
                .ReturnsAsync((Question)null);

            _questionRepoMock.Setup(x => x.CreateAsync(It.IsAny<Question>()))
                .ReturnsAsync((Question q) => q);

            _questionCategoryRepoMock.Setup(x => x.CheckQuestionCategoryExists(It.IsAny<Guid>(), command.CategoryId))
                .ReturnsAsync(false);

            _questionCategoryRepoMock.Setup(x => x.AddQuestionCategory(It.IsAny<Guid>(), command.CategoryId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Question created successfully.");
            result.Data.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenQuestionAlreadyExists_AndCategoryLinked()
        {
            // Arrange
            var existingQuestion = new Question
            {
                Id = Guid.NewGuid(),
                Text = "Existing Question",
                SkillLevel = SkillType.Intermediate,
                QuestionLevel = QuestionType.Medium
            };

            var command = new CreateQuestionCommand
            {
                Content = existingQuestion.Text,
                SkillLevel = (int)existingQuestion.SkillLevel,
                QuestionLevel = (int)existingQuestion.QuestionLevel,
                CategoryId = Guid.NewGuid()
            };

            _questionRepoMock.Setup(x => x.GetQuestionByContentAsync(command.Content))
                .ReturnsAsync(existingQuestion);

            _questionCategoryRepoMock.Setup(x => x.CheckQuestionCategoryExists(existingQuestion.Id, command.CategoryId))
                .ReturnsAsync(false);

            _questionCategoryRepoMock.Setup(x => x.AddQuestionCategory(existingQuestion.Id, command.CategoryId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().Be(existingQuestion.Id);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCreateFails()
        {
            // Arrange
            var command = new CreateQuestionCommand
            {
                Content = "Failing Question",
                SkillLevel = (int)SkillType.Advanced,
                QuestionLevel = (int)QuestionType.Hard,
                CategoryId = Guid.NewGuid()
            };

            _questionRepoMock.Setup(x => x.GetQuestionByContentAsync(command.Content))
                .ReturnsAsync((Question)null);

            _questionRepoMock.Setup(x => x.CreateAsync(It.IsAny<Question>()))
                .ReturnsAsync((Question)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Failed to create question.");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenExceptionThrown()
        {
            // Arrange
            var command = new CreateQuestionCommand
            {
                Content = "Exception Test",
                SkillLevel = 1,
                QuestionLevel = 1,
                CategoryId = Guid.NewGuid()
            };

            _questionRepoMock.Setup(x => x.GetQuestionByContentAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Database failure"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("An error occurred while creating the question.");
        }
    }
}

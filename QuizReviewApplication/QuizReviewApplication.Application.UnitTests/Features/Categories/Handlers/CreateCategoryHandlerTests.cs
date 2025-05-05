using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using QuizReviewApplication.Application.Features.Categories.Commands;
using QuizReviewApplication.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.UnitTests.Features.Categories.Handlers
{
    public class CreateCategoryHandlerTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly ILogger<CreateCategoryHandler> _logger;
        private readonly CreateCategoryHandler _handler;

        public CreateCategoryHandlerTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _logger = new LoggerFactory().CreateLogger<CreateCategoryHandler>();
            _handler = new CreateCategoryHandler(_categoryRepositoryMock.Object, _logger);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenCategoryCreated()
        {
            // Arrange
            var expectedId = Guid.NewGuid();
            var command = new CreateCategoryCommand
            {
                Name = "Science",
                Value = "2"
            };
            _categoryRepositoryMock
                .Setup(repo => repo.CreateCategoryAsync(command.Name, command.Value))
                .ReturnsAsync(expectedId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Category created successfully.");
            result.Data.Should().Be(expectedId);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCategoryIdIsEmpty()
        {
            // Arrange
            var command = new CreateCategoryCommand
            {
                Name = "Science",
                Value = "2"
            };
            _categoryRepositoryMock
                .Setup(repo => repo.CreateCategoryAsync(command.Name, command.Value))
                .ReturnsAsync(Guid.Empty);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Failed to create category.");
        }
    }
}

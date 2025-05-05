using AutoMapper;
using FluentAssertions;
using Moq;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.QuestionAnswers.Commands.CreateQuestionAnswer;
using QuizReviewApplication.Application.Repositories;
using QuizReviewApplication.Application.Services;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.UnitTests.Features.QuestionAnswers.Handlers
{
    public class CreateQuestionAnswerHandlerTests
    {
        private readonly Mock<IAnswerRepository> _answerRepoMock;
        private readonly Mock<IQuestionRepository> _questionRepoMock;
        private readonly Mock<IAiServices> _aiServiceMock;
        private readonly IMapper _mapper;
        private readonly CreateQuestionAnswerHandler _handler;

        public CreateQuestionAnswerHandlerTests()
        {
            _answerRepoMock = new Mock<IAnswerRepository>();
            _questionRepoMock = new Mock<IQuestionRepository>();
            _aiServiceMock = new Mock<IAiServices>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateQuestionAnswerCommand, Answer>();
            });
            _mapper = config.CreateMapper();

            _handler = new CreateQuestionAnswerHandler(
                _answerRepoMock.Object,
                _questionRepoMock.Object,
                _aiServiceMock.Object,
                _mapper
            );
        }

        [Fact]
        public async Task Handle_ShouldCreateAnswer_WhenAllIsValid()
        {
            // Arrange
            var questionId = Guid.NewGuid();
            var command = new CreateQuestionAnswerCommand
            {
                QuestionId = questionId,
                Text = "Correct answer"
            };
            var question = new Question { Id = questionId, Text = "What is AI?" };
            var aiResult = new EvaluationResponseDto { Score = 3, Feedback = "Excellent" };
            var savedAnswer = new Answer { Id = Guid.NewGuid(), Marks = 3, Feedback = "Excellent" };

            _questionRepoMock.Setup(x => x.GetQuestionByIdAsync(questionId)).ReturnsAsync(question);
            _aiServiceMock.Setup(x => x.GetResponseFromAi(It.IsAny<string>())).ReturnsAsync(JsonSerializer.Serialize(aiResult));
            _answerRepoMock.Setup(x => x.CreateAsync(It.IsAny<Answer>())).ReturnsAsync(savedAnswer);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Data.Should().Be(savedAnswer.Id.ToString());
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenQuestionNotFound()
        {
            // Arrange
            var command = new CreateQuestionAnswerCommand { QuestionId = Guid.NewGuid(), Text = "Some answer" };
            _questionRepoMock.Setup(x => x.GetQuestionByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Question)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Question not found.");
        }

        [Fact]
        public async Task Handle_ShouldSetDefaultMarks_WhenAiResponseIsNull()
        {
            // Arrange
            var questionId = Guid.NewGuid();
            var command = new CreateQuestionAnswerCommand { QuestionId = questionId, Text = "Answer with AI fail" };
            var question = new Question { Id = questionId, Text = "Explain gravity." };
            var savedAnswer = new Answer { Id = Guid.NewGuid(), Marks = 0, Feedback = "No feedback provided" };

            _questionRepoMock.Setup(x => x.GetQuestionByIdAsync(questionId)).ReturnsAsync(question);
            _aiServiceMock.Setup(x => x.GetResponseFromAi(It.IsAny<string>())).ReturnsAsync("invalid json");
            _answerRepoMock.Setup(x => x.CreateAsync(It.IsAny<Answer>())).ReturnsAsync(savedAnswer);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Answer created successfully.");
            result.Data.Should().Be(savedAnswer.Id.ToString());
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenSaveFails()
        {
            // Arrange
            var questionId = Guid.NewGuid();
            var command = new CreateQuestionAnswerCommand { QuestionId = questionId, Text = "Saving fails" };
            var question = new Question { Id = questionId, Text = "Explain polymorphism." };
            var aiResponse = new EvaluationResponseDto { Score = 2, Feedback = "Good attempt" };

            _questionRepoMock.Setup(x => x.GetQuestionByIdAsync(questionId)).ReturnsAsync(question);
            _aiServiceMock.Setup(x => x.GetResponseFromAi(It.IsAny<string>()))
                          .ReturnsAsync(JsonSerializer.Serialize(aiResponse));
            _answerRepoMock.Setup(x => x.CreateAsync(It.IsAny<Answer>()))
                           .ReturnsAsync((Answer)null); // Simulate failure

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Failed to create answer.");
        }
    }
}

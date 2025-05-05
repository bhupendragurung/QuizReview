using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.Auth.ForgotPassword.Commands;
using QuizReviewApplication.Application.Interfaces;
using QuizReviewApplication.Application.Services;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.UnitTests.Features.Auth.Handlers
{
    public class ForgotPasswordHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly ForgortPasswordHandler _handler;
        public ForgotPasswordHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _emailServiceMock = new Mock<IEmailService>();
            _configurationMock = new Mock<IConfiguration>();
            _handler = new ForgortPasswordHandler(_userServiceMock.Object, _configurationMock.Object, _emailServiceMock.Object);
        }
        [Fact]
        public async Task Handle_ShouldReturnFailureResponse_whenUserNotFound()
        {
            // Arrange
            var dto = new ForgotPasswordDto { Email = "Test@gmail.com" };
            _userServiceMock.Setup(x => x.FindByEmailAsync(dto.Email)).ReturnsAsync((ApplicationUser)null);
            var command = new ForgotPasswordCommand { forgotPasswordDto = dto };
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("User not found.");
        }
        [Fact]
        public async Task Handle_ShoulReturnFailureResponse_WhenEmailNotConfirmed()
        {
            // Arrange
           
            var user = new ApplicationUser { Email = "test@gmail.com" };
            _userServiceMock.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            _userServiceMock.Setup(x => x.IsEmailConfirmedAsync(user)).ReturnsAsync(false);
            var command = new ForgotPasswordCommand { forgotPasswordDto = new ForgotPasswordDto { Email = user.Email } };
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Email not confirmed.");

        }
        [Fact]
        public async Task Handle_ShouldSendEmailAndReturnSuccessResponse_WhenValidUser()
        {
            var user = new ApplicationUser { Email = "test@gmail.com" };
            _userServiceMock.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            _userServiceMock.Setup(x => x.IsEmailConfirmedAsync(user)).ReturnsAsync(true);
            _userServiceMock.Setup(x => x.GeneratePasswordResetTokenAsync(user)).ReturnsAsync("token");
            _configurationMock.Setup(x => x["FrontendBaseUrl"]).Returns("http://localhost:3000");
            _emailServiceMock.Setup(x => x.SendEmailAsync(user.Email, It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);   
            var command = new ForgotPasswordCommand { forgotPasswordDto = new ForgotPasswordDto { Email = user.Email } };
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Password reset link sent to your email.");
            _emailServiceMock.Verify(x => x.SendEmailAsync(user.Email, "Reset Password", It.IsAny<string>()), Times.Once);
   ;
        }
    }
}

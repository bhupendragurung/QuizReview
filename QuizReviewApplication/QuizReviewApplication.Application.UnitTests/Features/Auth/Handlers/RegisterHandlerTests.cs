using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.Auth.Register.Commands;
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
    public class RegisterHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;  
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<ILogger<RegisterHandler>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly RegisterHandler _handler;
        public RegisterHandlerTests()
        {
                _userServiceMock = new Mock<IUserService>();
            _emailServiceMock = new Mock<IEmailService>();
            _loggerMock = new Mock<ILogger<RegisterHandler>>();
            _configurationMock = new Mock<IConfiguration>();
            _handler = new RegisterHandler(_emailServiceMock.Object, _configurationMock.Object, _loggerMock.Object, _userServiceMock.Object);
        }
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenRegistrationFail()
        {
            //arrange
            var user = new ApplicationUser();
            var command = new RegisterCommand
            {
                RegisterDto = new Dtos.RegisterDto
                {
                    Username = "failUser",
                    Password = "fail",
                    Email="fail@gmail.com"
                }
            };
         
            _userServiceMock.Setup(x=>x.CreateUserAsync(command.RegisterDto)).ReturnsAsync((IdentityResult.Failed(new IdentityError { Description = "User Registration Failed." }), user));
            //act
            var result = await _handler.Handle(command, CancellationToken.None);
            //assert
           result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("User Registration Failed.");
            _emailServiceMock.Verify(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenRegistrationSucceeds()
        {
            //arrange
            var registerDto = new RegisterDto
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "Password123!"
            };
            var user = new ApplicationUser { UserName = registerDto.Username, Email = registerDto.Email };
            var identityResult = IdentityResult.Success;
        var command = new RegisterCommand
        {
                RegisterDto = registerDto
            };
            _userServiceMock.Setup(x => x.CreateUserAsync(registerDto)).ReturnsAsync((identityResult, user));
            _userServiceMock.Setup(x=>x.GenerateEmailConfirmationTokenAsync(user)).ReturnsAsync("token");

            _configurationMock.Setup(x => x["FrontendBaseUrl"]).Returns("http://localhost:3000");
            //act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().Be("User registered successfully. Please check your email to verify your account.");

            _emailServiceMock.Verify(x => x.SendEmailAsync(
                registerDto.Email,
                "Verify Your Email",
                It.Is<string>(s => s.Contains("http://localhost:3000/User/VerifyEmail"))), Times.Once);
        }
    }
}

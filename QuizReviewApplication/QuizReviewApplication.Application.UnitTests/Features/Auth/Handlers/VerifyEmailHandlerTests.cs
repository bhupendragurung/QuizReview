using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using QuizReviewApplication.Application.Features.Auth.VerifyEmail.Command;
using QuizReviewApplication.Application.Interfaces;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.UnitTests.Features.Auth.Handlers
{
    public class VerifyEmailHandlerTests
    {
        private readonly VerifyEmailHandler _handler;
        private readonly Mock<IUserService> _userServiceMock;
        public VerifyEmailHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _handler = new VerifyEmailHandler(_userServiceMock.Object);
        }
        [Fact]
        public async Task Should_ReturnFailure_WhenUserNotFound()
        {
            // Arrange
            _userServiceMock.Setup(x => x.FindByIdAsync("user123"))
                .ReturnsAsync((ApplicationUser)null!);

            var command = new VerifyEmailCommand { UserId = "user123", Token = "dummy" };
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
              
            result.Success.Should().BeFalse();
            result.Message.Should().Be("User not found");
        }

        [Fact]
        public async Task Should_ReturnSuccess_WhenEmailConfirmed()
        {
            // Arrange
            var user = new ApplicationUser { Id = "user123", Email = "test@example.com" };
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes("valid-token"));

            _userServiceMock.Setup(x => x.FindByIdAsync(user.Id)).ReturnsAsync(user);
            _userServiceMock.Setup(x => x.ConfirmEmailAsync(user, "valid-token")).ReturnsAsync(IdentityResult.Success);

           
            var command = new VerifyEmailCommand { UserId = user.Id, Token = token };
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
                
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Email verified successfully");
        }

        [Fact]
        public async Task Should_ReturnFailure_WhenEmailConfirmationFails()
        {// Arrange
            var user = new ApplicationUser { Id = "user123" };
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes("bad-token"));

            _userServiceMock.Setup(x => x.FindByIdAsync(user.Id)).ReturnsAsync(user);
            _userServiceMock.Setup(x => x.ConfirmEmailAsync(user, "bad-token"))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Invalid token" }));

            var command = new VerifyEmailCommand { UserId = user.Id, Token = token };
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Email verification failed");
        }

        [Fact]
        public async Task Should_ReturnFailure_WhenTokenIsInvalidBase64()
        {// Arrange
            var user = new ApplicationUser { Id = "user123" };

            _userServiceMock.Setup(x => x.FindByIdAsync(user.Id)).ReturnsAsync(user);

            var command = new VerifyEmailCommand { UserId = user.Id, Token = "%%%invalidbase64" };
          //act
            var result = await _handler.Handle(command, CancellationToken.None);
            //assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Invalid token format");
        }
    }
}

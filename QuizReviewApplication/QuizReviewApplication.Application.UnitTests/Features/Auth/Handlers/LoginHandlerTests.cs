using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.Auth.Login.Commands;
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
    public class LoginHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ISignInService> _signInServiceMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly LoginHandler _handler;
        public LoginHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _signInServiceMock = new Mock<ISignInService>();
            _tokenServiceMock = new Mock<ITokenService>();
            _emailServiceMock = new Mock<IEmailService>();
            _handler = new LoginHandler(_userServiceMock.Object, _signInServiceMock.Object, _tokenServiceMock.Object, _emailServiceMock.Object);
        }
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
        {
            // Arrange
            var request = new LoginCommand
            {
                LoginDto = new LoginDto
                {
                    Username = "testuser",
                    Password = "password"
                }
            };
            _userServiceMock.Setup(x=>x.FindByNameAsync(request.LoginDto.Username))
                .ReturnsAsync((ApplicationUser)null);
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("User not Found");
        }
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenEmailNotConfirmed()
        {
            // Arrange
            var user= new ApplicationUser
            {
                EmailConfirmed = false
            };
            var request = new LoginCommand
            {
                LoginDto = new LoginDto
                {
                    Username = "testuser",
                    Password = "password"
                }
            };
            _userServiceMock.Setup(x=>x.FindByNameAsync(request.LoginDto.Username))
                .ReturnsAsync(user);
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);
            // Assert
            result.Should().NotBeNull();
                result.Success.Should().BeFalse();
            result.Message.Should().Be("Email not confirmed");
           
        }
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenAccountIsLocked()
        {
            //arrange
            var user = new ApplicationUser
            {
                EmailConfirmed = true,
            };
            var request = new LoginCommand
            {
                LoginDto = new LoginDto
                {
                    Username = "testuser",
                    Password = "password"
                }
            };
            _userServiceMock.Setup(x => x.FindByNameAsync(request.LoginDto.Username))
                .ReturnsAsync(user);
            _userServiceMock.Setup(x => x.IsLockedOutAsync(user)).ReturnsAsync(true);
            //act
            var result = await _handler.Handle(request, CancellationToken.None);
            //assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Account is temporarily locked");

        }
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenCredentialsAreValid()
        {
            // Arrange
            var user = new ApplicationUser
            {
                EmailConfirmed = true,
            };
            var request = new LoginCommand
            {
                LoginDto = new LoginDto
                {
                    Username = "testuser",
                    Password = "password"
                }
            };
            _userServiceMock.Setup(x => x.FindByNameAsync(request.LoginDto.Username))
                .ReturnsAsync(user);
            _userServiceMock.Setup(x => x.IsLockedOutAsync(user)).ReturnsAsync(false);
            _signInServiceMock.Setup(x=>x.PasswordSignInAsync(user,request.LoginDto.Password)).ReturnsAsync(SignInResult.Success);
            _userServiceMock.Setup(x => x.ResetAccessFailedCountAsync(user)).Returns(Task.CompletedTask);
            _tokenServiceMock.Setup(x => x.CreateToken(user)).ReturnsAsync("token");    
            _tokenServiceMock.Setup(x=>x.GenerateRefreshToken("123.0.0.1")).ReturnsAsync(new RefreshToken { Token="refresh"});
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);
            // Assert
            result.Should().NotBeNull();
                
            result.Success.Should().BeTrue();
            result.Message.Should().Be("User Login Successfully");
        }
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenLockedOutAfterFailedAttempts()
        {
            var user = new ApplicationUser { Email = "user@example.com", EmailConfirmed = true };
            var dto = new LoginDto { Username = "user", Password = "wrongpass" };

            _userServiceMock.Setup(x => x.FindByNameAsync(dto.Username)).ReturnsAsync(user);
            _userServiceMock.Setup(x => x.IsLockedOutAsync(user)).ReturnsAsync(false);
            _signInServiceMock.Setup(x => x.PasswordSignInAsync(user, dto.Password)).ReturnsAsync(SignInResult.LockedOut);

            var result = await _handler.Handle(new LoginCommand { LoginDto = dto }, CancellationToken.None);

            result.Success.Should().BeFalse();
            result.Message.Should().Be("Your account is locked.");

            _emailServiceMock.Verify(x => x.SendEmailAsync(user.Email, "Account Locked",
                It.Is<string>(s => s.Contains("temporarily locked"))), Times.Once);
        }
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenPasswordIsInvalid()
        {
            var user = new ApplicationUser { EmailConfirmed = true };
            var dto = new LoginDto { Username = "user", Password = "wrongpass" };

            _userServiceMock.Setup(x => x.FindByNameAsync(dto.Username)).ReturnsAsync(user);
            _userServiceMock.Setup(x => x.IsLockedOutAsync(user)).ReturnsAsync(false);
            _signInServiceMock.Setup(x => x.PasswordSignInAsync(user, dto.Password)).ReturnsAsync(SignInResult.Failed);

            var result = await _handler.Handle(new LoginCommand { LoginDto = dto }, CancellationToken.None);

            result.Success.Should().BeFalse();
            result.Message.Should().Be("Invalid credentials.");
        }
    }
}

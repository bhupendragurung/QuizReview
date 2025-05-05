using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.Auth.ResetPassword.Command;
using QuizReviewApplication.Application.Interfaces;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.UnitTests.Features.Auth.Handlers
{
    public class ResetPasswordHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly ResetPasswordHandler _handler;
        public ResetPasswordHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _handler = new ResetPasswordHandler(_userServiceMock.Object);
        }
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
        {
            // Arrange
            var resetPasswordDto = new ResetPasswordDto
            {
                Email = "notfound@example.com",
                Token = Convert.ToBase64String(Encoding.UTF8.GetBytes("token")),
                NewPassword = "AnyPassword123"
            };
            _userServiceMock.Setup(x => x.FindByEmailAsync(resetPasswordDto.Email))
                .ReturnsAsync((ApplicationUser?)null);
            var command = new ResetPasswordCommand { resetPasswordDto = resetPasswordDto };
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("User not found.");
        }
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenResetPasswordSucceeds()
        {
            // Arrange
            var resetPasswordDto = new ResetPasswordDto
            {
                Email = "test@example.com",
                Token = Convert.ToBase64String(Encoding.UTF8.GetBytes("valid-token")),
                NewPassword = "NewPassword123!"
            };
            var user = new ApplicationUser { Email = resetPasswordDto.Email };
         
              _userServiceMock.Setup(x => x.FindByEmailAsync(resetPasswordDto.Email))
                .ReturnsAsync(user);
            _userServiceMock.Setup(x => x.PasswordResetAsync(user, "valid-token", resetPasswordDto.NewPassword)).ReturnsAsync(IdentityResult.Success);
            var command = new ResetPasswordCommand { resetPasswordDto = resetPasswordDto };
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.Should().NotBeNull();
                
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Password reset successfully.");


        }
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenResetPasswordFails()
        {
            // Arrange  
            var resetPasswordDto = new ResetPasswordDto {
                Email = "test@example.com",
                Token = Convert.ToBase64String(Encoding.UTF8.GetBytes("bad-token")),
                NewPassword = "bad"
            };
            var user = new ApplicationUser { Email = resetPasswordDto.Email };
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes("valid-token"));
            resetPasswordDto.Token = token;
            var errors = new List<IdentityError>
            {
                new IdentityError { Description = "Password reset failed." }
            };  
            _userServiceMock.Setup(x => x.FindByEmailAsync(resetPasswordDto.Email))
                .ReturnsAsync(user);
            _userServiceMock.Setup(x => x.PasswordResetAsync(user, "bad-token", resetPasswordDto.NewPassword)).ReturnsAsync(IdentityResult.Failed(errors.ToArray()));
            var command = new ResetPasswordCommand { resetPasswordDto = resetPasswordDto };
            // Act
                var result = await _handler.Handle(command, CancellationToken.None);
            // Assert   
                result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Password doesnot reset");
            result.Errors.Should().NotBeNullOrEmpty();

        }
    }
}

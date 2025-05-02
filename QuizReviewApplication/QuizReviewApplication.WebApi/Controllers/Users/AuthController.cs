using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.Auth.ForgotPassword.Commands;
using QuizReviewApplication.Application.Features.Auth.Login.Commands;
using QuizReviewApplication.Application.Features.Auth.Register.Commands;
using QuizReviewApplication.Application.Features.Auth.ResetPassword.Command;
using QuizReviewApplication.Application.Features.Auth.VerifyEmail.Command;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Application.Services;
using QuizReviewApplication.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizReviewApplication.WebApi.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ApiResponse<string>.FailureResponse("Invalid registration input"));
            var result = await _sender.Send(new RegisterCommand { RegisterDto = registerDto });
            return result.Success ? Ok(result) : BadRequest(result);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailureResponse("Invalid input"));

            var result = await _sender.Send(new LoginCommand { LoginDto = model });
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            var result = await _sender.Send(new VerifyEmailCommand { Token = token, UserId = userId });
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailureResponse("Invalid input"));

            var result = await _sender.Send(new ForgotPasswordCommand { forgotPasswordDto = forgotPasswordDto });
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailureResponse("Invalid input"));

            var result = await _sender.Send(new ResetPasswordCommand { resetPasswordDto = resetPasswordDto });
            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}

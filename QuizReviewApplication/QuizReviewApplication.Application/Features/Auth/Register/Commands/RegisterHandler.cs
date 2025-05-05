using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Application.Interfaces;
using QuizReviewApplication.Application.Services;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Auth.Register.Commands
{
    public class RegisterHandler:IRequestHandler<RegisterCommand, ApiResponse<string>>
    {
       
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private ILogger<RegisterHandler> _logger;
        private readonly IUserService _userService;

        public RegisterHandler( IEmailService emailService,  IConfiguration configuration, ILogger<RegisterHandler> logger,IUserService userService)
        {
          
            _emailService = emailService;
            _configuration = configuration;
            _logger = logger;
            _userService = userService;
        }
        public async Task<ApiResponse<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling registration for user: {Username}", request.RegisterDto.Username);
            var registerDto = request.RegisterDto;

          
            var (result,user) = await _userService.CreateUserAsync(registerDto);
            if (!result.Succeeded)
            {
                _logger.LogError("Registration failed for user: {Username}. Errors: {Errors}", registerDto.Username, result.Errors);
                return ApiResponse<string>.FailureResponse("User Registration Failed.",result.Errors.Select(e => e.Description).ToList());
            }
     
            var token = await _userService.GenerateEmailConfirmationTokenAsync(user);


            // Create verification link
            var frontendUrl = _configuration["FrontendBaseUrl"];
            var verificationLink = $"{frontendUrl}/User/VerifyEmail?userId={user.Id}&token={token}";

            // Send email (implement IEmailService)
            await _emailService.SendEmailAsync(user.Email, "Verify Your Email",
                $"Please verify your email by clicking <a href=\"{verificationLink}\">here</a>.");
            _logger.LogInformation("Verification email sent to user: {Email}", user.Email);
            return ApiResponse<string>.SuccessResponse("User registered successfully. Please check your email to verify your account.");


        }
    
    
    }
}

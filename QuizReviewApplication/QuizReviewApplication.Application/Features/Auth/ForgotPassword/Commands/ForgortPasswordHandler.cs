using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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

namespace QuizReviewApplication.Application.Features.Auth.ForgotPassword.Commands
{
    public class ForgortPasswordHandler:IRequestHandler<ForgotPasswordCommand, ApiResponse<string>>
    {
        
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public ForgortPasswordHandler(IUserService userService, IConfiguration configuration,  IEmailService emailService)
        {
           
            _userService = userService;
            _configuration = configuration;
            _emailService = emailService;
        }
        public async Task<ApiResponse<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
          
            var user = await _userService.FindByEmailAsync(request.forgotPasswordDto.Email);
            if (user == null  )
                return ApiResponse<string>.FailureResponse("User not found.");
            // Check if email is confirmed
            if (!(await _userService.IsEmailConfirmedAsync(user))){
                return ApiResponse<string>.FailureResponse("Email not confirmed.");
            }
            // Generate password reset token
            var token = await _userService.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

            // Create password reset link

            var frontendUrl = _configuration["FrontendBaseUrl"];
            var resetLink = $"{frontendUrl}/User/ResetPassword?email={user.Email}&token={encodedToken}";

            // Send email (implement IEmailService)
            await _emailService.SendEmailAsync(user.Email, "Reset Password",
                               $"Please reset your password by clicking <a href=\"{resetLink}\">here</a>.");

            return ApiResponse<string>.SuccessResponse( "Password reset link sent to your email." );
        }
 
    }
    
    
}

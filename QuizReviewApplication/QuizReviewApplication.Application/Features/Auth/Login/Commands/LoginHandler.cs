using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
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

namespace QuizReviewApplication.Application.Features.Auth.Login.Commands
{
    public class LoginHandler:IRequestHandler<LoginCommand, ApiResponse<string>>
    {
        private readonly IUserService _userService;
        private readonly ISignInService _signInService;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        public LoginHandler(IUserService userService,ISignInService signInService,ITokenService tokenService, IEmailService emailService)
        {
            _userService = userService;
            _signInService = signInService;
            _tokenService = tokenService;
            _emailService = emailService;
        }
        public async Task<ApiResponse<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
         
            var model = request.LoginDto;
            var user = await _userService.FindByNameAsync(model.Username);

            if (user == null) return ApiResponse<string>.FailureResponse( "Unauthorized");

            if (!user.EmailConfirmed) return  ApiResponse<string>.FailureResponse("Email not confirmed");
            if (await _userService.IsLockedOutAsync(user)) return ApiResponse<string>.FailureResponse("Account is temporarily locked");

            var result = await _signInService.PasswordSignInAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userService.ResetAccessFailedCountAsync(user);
                var token = await _tokenService.CreateToken(user);
                var refreshToken = await _tokenService.GenerateRefreshToken(ipAddress:"123.0.0.1");
                /// we need to send refresh token to clientnew AuthResponseDto
        //        {
        //            Token = jwtToken,
        //RefreshToken = refreshToken.Token
        //        };
                return  ApiResponse<string>.SuccessResponse("User Login Successfully", token );
            }

            if (result.IsLockedOut)
            {
                await _emailService.SendEmailAsync(user.Email, "Account Locked",
                    "Your account has been temporarily locked due to multiple failed login attempts. Please try again later.");
                return ApiResponse<string>.FailureResponse("Your account is locked.");
            }

            return ApiResponse<string>.FailureResponse("Invalid credentials.");

        }
    }
    
    
}

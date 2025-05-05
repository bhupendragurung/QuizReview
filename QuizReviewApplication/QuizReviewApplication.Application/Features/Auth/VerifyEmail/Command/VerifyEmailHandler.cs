using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Application.Interfaces;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Auth.VerifyEmail.Command
{
    public class VerifyEmailHandler:IRequestHandler<VerifyEmailCommand, ApiResponse<string>>
    {
        private readonly IUserService _userService;

        public VerifyEmailHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<ApiResponse<string>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.UserId);
            if (user == null) return ApiResponse<string>.FailureResponse( "User not found");
            request.Token = request.Token.Replace(" ", "+");
            var decodedBytes = Convert.FromBase64String(request.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedBytes);
            var result = await _userService.ConfirmEmailAsync(user, decodedToken);

            if (result.Succeeded)
            {
                return ApiResponse<string>.SuccessResponse( "Email verified successfully");
            }
            else
            {
                return ApiResponse<string>.FailureResponse("Email verification failed");
            }
        }
    }
    
}

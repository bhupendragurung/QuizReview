using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Application.Interfaces;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Auth.ResetPassword.Command
{
    public class ResetPasswordHandler: IRequestHandler<ResetPasswordCommand, ApiResponse<string>>
    {
        private readonly IUserService _userService;

        public ResetPasswordHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<ApiResponse<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
             
            var user = await _userService.FindByEmailAsync(request.resetPasswordDto.Email);
            if (user == null) return ApiResponse<string>.FailureResponse("User not found.");

            request.resetPasswordDto.Token = request.resetPasswordDto.Token.Replace(" ", "+");
            var decodedBytes = Convert.FromBase64String(request.resetPasswordDto.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedBytes);

            var result = await _userService.PasswordResetAsync(user, decodedToken, request.resetPasswordDto.NewPassword);
            if (result.Succeeded)
            {
                return ApiResponse<string>.SuccessResponse ("Password reset successfully.");
            }
            else
            {
                return ApiResponse<string>.FailureResponse("Password doesnot reset", result.Errors.Select(e=>e.Description).ToList());
            }
        }
    }
    
}

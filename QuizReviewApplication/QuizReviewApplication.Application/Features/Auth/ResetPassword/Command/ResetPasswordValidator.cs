using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Auth.ResetPassword.Command
{
    public class ResetPasswordValidator: AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.resetPasswordDto.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.resetPasswordDto.NewPassword)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
            RuleFor(x => x.resetPasswordDto.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required")
                .Equal(x => x.resetPasswordDto.NewPassword).WithMessage("Passwords do not match");
            RuleFor(x => x.resetPasswordDto.Token)
                .NotEmpty().WithMessage("Token is required");
        }
    }
    

    
}

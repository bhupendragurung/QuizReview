using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Auth.VerifyEmail.Command
{
    public class VerifyEmailValidator:AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("Token is required.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.");
        }
    }
    
    
}

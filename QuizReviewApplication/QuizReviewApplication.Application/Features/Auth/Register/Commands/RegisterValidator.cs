using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Auth.Register.Commands
{
    public class RegisterValidator:AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.RegisterDto.Username)
                .NotEmpty()
                .WithMessage("Username is required.");

            RuleFor(x => x.RegisterDto.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("A valid email is required.");

            RuleFor(x => x.RegisterDto.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.");
        }
    }
   
}

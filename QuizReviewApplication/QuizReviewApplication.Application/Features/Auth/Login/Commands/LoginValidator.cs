using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Auth.Login.Commands
{
    public class LoginValidator:AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.LoginDto.Username)
                .NotEmpty()
                .WithMessage("Username is required.");

            RuleFor(x => x.LoginDto.Password)
                .NotEmpty()
                .WithMessage("Password is required.");
        }
    }
    
    
}

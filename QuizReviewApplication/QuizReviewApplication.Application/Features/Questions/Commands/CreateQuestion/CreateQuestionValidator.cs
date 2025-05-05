using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionValidator:AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionValidator()
        {
            RuleFor(q => q.Content).NotEmpty().NotNull();
            RuleFor(q => q.SkillLevel).GreaterThan(0);
            RuleFor(q => q.QuestionLevel).GreaterThan(0);
            RuleFor(q => q.CategoryId).NotEmpty().NotNull();

        }
    }
}

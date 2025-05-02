using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.QuestionAnswers.Commands.CreateQuestionAnswer
{
    public class CreateQuestionAnswerValidator:AbstractValidator<CreateQuestionAnswerCommand>
    {
        public CreateQuestionAnswerValidator()
        {
            RuleFor(q => q.Text).NotEmpty().NotNull();
        }
    }
}

using FluentValidation;

namespace QuizReviewApplication.Application.Features.Questions.Queries.GetQuestionById
{
    public class GetQuestionByIdValidator:AbstractValidator<GetQuestionByIdQuery>
    {
        public GetQuestionByIdValidator()
        {
            RuleFor(q => q.QuestionId)
            .NotEmpty();
        }
    }
}
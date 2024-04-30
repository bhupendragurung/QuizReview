using QuizReviewApplication.Application.Common.Response;
using QuizReviewApplication.Application.Dtos;

namespace QuizReviewApplication.Application.Features.Questions.Queries.GetQuestionById
{
    public class GetQuestionByIdResponse:BaseResponse
    {
        public QuestionDto Question { get; set; }
    }
}
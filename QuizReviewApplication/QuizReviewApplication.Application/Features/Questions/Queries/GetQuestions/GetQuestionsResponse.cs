using QuizReviewApplication.Application.Common.Response;
using QuizReviewApplication.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Questions.Queries.GetQuestions
{
    public class GetQuestionsResponse:BaseResponse
    {
        public List<QuestionDto> Questions { get; set; }
    }
}

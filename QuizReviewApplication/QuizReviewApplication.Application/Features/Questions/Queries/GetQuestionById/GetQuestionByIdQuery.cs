using MediatR;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Questions.Queries.GetQuestionById
{
    public class GetQuestionByIdQuery:IRequest<ApiResponse<QuestionDto>>
    {
        public Guid QuestionId { get; set; }
    }
}

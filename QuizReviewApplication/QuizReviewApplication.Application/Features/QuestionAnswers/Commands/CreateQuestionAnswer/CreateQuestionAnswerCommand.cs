using MediatR;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.QuestionAnswers.Commands.CreateQuestionAnswer
{
    public class CreateQuestionAnswerCommand:IRequest<ApiResponse<string>>
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public int Marks { get; set; }

    }
}

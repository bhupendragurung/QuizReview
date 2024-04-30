using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Questions.Queries.GetQuestionById
{
    public class GetQuestionByIdQuery:IRequest<GetQuestionByIdResponse>
    {
        public Guid QuestionId { get; set; }
    }
}

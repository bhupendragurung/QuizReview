using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using QuizReviewApplication.Application.Dtos;

namespace QuizReviewApplication.Application.Questions.Queries.GetQuestions
{
    public class GetQuestionsQuery : IRequest<IEnumerable<QuestionDto>>
    {
    }
    

    
    
    
}

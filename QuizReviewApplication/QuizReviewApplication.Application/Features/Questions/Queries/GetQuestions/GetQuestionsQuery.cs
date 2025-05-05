using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Helper;

namespace QuizReviewApplication.Application.Features.Questions.Queries.GetQuestions
{
    public class GetQuestionsQuery : QuestionParams, IRequest<ApiResponse<PagedList<QuestionDto>>>
    {
        
    }





}

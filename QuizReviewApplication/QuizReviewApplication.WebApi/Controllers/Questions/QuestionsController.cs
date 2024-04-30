using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.Questions.Queries.GetQuestions;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Infrastructure.Data;

namespace QuizReviewApplication.WebApi.Controllers.Questions
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {

        private readonly ISender _sender;

        public QuestionsController(ISender sender)
        {
            _sender = sender;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GetQuestionsResponse>> GetQuestions()
        {
            return await _sender.Send(new GetQuestionsQuery());
          
        }
     

    }
}

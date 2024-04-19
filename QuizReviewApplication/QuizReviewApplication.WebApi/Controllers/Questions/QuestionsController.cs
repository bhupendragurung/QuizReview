using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Questions.Commands.CreateQuestion;
using QuizReviewApplication.Application.Questions.Queries.GetQuestions;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Domain.Repositories;
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
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestions()
        {
            var questions = await _sender.Send(new GetQuestionsQuery());
            return Ok(questions);
          
        }
     

    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Questions.Commands.CreateQuestion;
using QuizReviewApplication.Application.Questions.Queries.GetQuestions;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Domain.Repositories;
using QuizReviewApplication.Infrastructure.Data;

namespace QuizReviewApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        
        private readonly ISender _sender;

        public QuestionController(ISender sender)
        {
            _sender = sender;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestions()
        {
           
            var questions = _sender.Send(new GetQuestionsQuery());
            if (questions != null)
            {
                return Ok(questions);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<ActionResult<QuestionDto>> Create(CreateQuestionCommand command)
        {
            var createquestion = _sender.Send(command);
            if (createquestion != null)
            {
                return Ok(createquestion);
            }
            else
            {
                return BadRequest();
            }
            
        }

    }
}

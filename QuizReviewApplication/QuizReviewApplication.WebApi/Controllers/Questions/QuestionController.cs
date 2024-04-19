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
    public class QuestionController : ControllerBase
    {

        private readonly ISender _sender;

        public QuestionController(ISender sender)
        {
            _sender = sender;
        }
  
        [HttpPost]
        public async Task<ActionResult<QuestionDto>> Create(CreateQuestionCommand command)
        {
            var createQuestion = await _sender.Send(command);
            return Ok(createQuestion);
           
        }

    }
}

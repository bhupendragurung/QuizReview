using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.Questions.Commands.CreateQuestion;
using QuizReviewApplication.Application.Features.Questions.Queries.GetQuestionById;
using QuizReviewApplication.Application.Features.Questions.Queries.GetQuestions;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Infrastructure.Data;

namespace QuizReviewApplication.WebApi.Controllers.Questions
{
 
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class QuestionController : ControllerBase
    {

        private readonly ISender _sender;

        public QuestionController(ISender sender)
        {
            _sender = sender;
        }
  
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Guid>>> Create(CreateQuestionCommand command)
        {
          var result= await _sender.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<QuestionDto>>> GetQuestionById(Guid id)
        {
            var result = await _sender.Send(new GetQuestionByIdQuery() { QuestionId = id });
            return result.Success ? Ok(result) : NotFound(result);

        }
    }
}

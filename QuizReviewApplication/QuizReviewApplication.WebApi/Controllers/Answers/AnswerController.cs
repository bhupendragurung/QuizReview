using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.QuestionAnswers.Commands.CreateQuestionAnswer;
using QuizReviewApplication.Application.Features.QuestionAnswers.Queries.GetQuestionAnswer;
using QuizReviewApplication.Application.Features.Questions.Commands.CreateQuestion;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Domain.Entities;

namespace QuizReviewApplication.WebApi.Controllers.Answers
{
   
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly ISender _sender;

        public AnswerController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create(CreateQuestionAnswerCommand command)
        {
            var result = await _sender.Send(command);

            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<AnswerDto>>>> GetAll()
        {
            var result = await _sender.Send(new GetAnswerQuery());

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.Questions.Commands.CreateQuestion;
using QuizReviewApplication.Application.Features.Questions.Queries.GetQuestionById;
using QuizReviewApplication.Domain.Entities;
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
        public async Task<ActionResult<CreateQuestionResponse>> Create(CreateQuestionCommand command)
        {
          return await _sender.Send(command);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetQuestionByIdResponse>> GetQuestionById(Guid id)
        {
            return await _sender.Send(new GetQuestionByIdQuery() { QuestionId=id});

        }
    }
}

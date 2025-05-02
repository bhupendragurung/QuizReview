using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizReviewApplication.Application.Features.Questions.Queries.GetQuestions;
using QuizReviewApplication.Application.Features.Users.Queries;

namespace QuizReviewApplication.WebApi.Controllers
{
  
    [ApiController]
    [Route("api/v{version:apiVersion}/Users")]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;
        public UsersController(ISender sender)
        {
            _sender = sender;
        }
        [HttpGet]
        public async Task<ActionResult<string>> GetToken()
        {
            return await _sender.Send(new GetTokenQuery());

        }
    }
}

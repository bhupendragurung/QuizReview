using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizReviewApplication.Application.Questions.Commands.CreateQuestion;
using QuizReviewApplication.Application.Questions.Queries.GetQuestions;
using QuizReviewApplication.Domain.Repositories;
using QuizReviewApplication.Infrastructure.Data;

namespace QuizReviewApplication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        
        private readonly ISender _sender;
        private readonly IQuestionRepository _questionRepository;

        public QuestionController(ISender sender,IQuestionRepository questionRepository)
        {
            _sender = sender;
            _questionRepository = questionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetQuestions()
        {
           //var result= await _questionRepository.GetAllQuestions();
           // return Ok( result);
            var questions = _sender.Send(new GetQuestionsQuery());
            return Ok(questions);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionCommand command)
        {
            var createquestion = _sender.Send(command);
            return Ok(createquestion);
        }

    }
}

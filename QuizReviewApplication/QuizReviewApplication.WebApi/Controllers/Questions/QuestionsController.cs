using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.Questions.Queries.GetQuestions;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Infrastructure.Data;
using QuizReviewApplication.WebApi.Extensions;

namespace QuizReviewApplication.WebApi.Controllers.Questions
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    
    public class QuestionsController : ControllerBase
    {

        private readonly ISender _sender;

        public QuestionsController(ISender sender)
        {
            _sender = sender;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse< PagedList<QuestionDto>>>> GetQuestions([FromQuery] QuestionParams questionParams)
        {
            var query = new GetQuestionsQuery
            {
                PageNumber = questionParams.PageNumber,
                PageSize = questionParams.PageSize,
                SkillLevel = questionParams.SkillLevel,
                QuestionLevel = questionParams.QuestionLevel,
                Category = questionParams.Category,
                Search = questionParams.Search
            };

            var result = await _sender.Send(query);

            if (result == null)
            {
                // In case of null result (error or empty), we return a failure response
                return BadRequest(result);
            }

            // Add pagination header to the response
            Response.AddPaginationHeader(new PaginationHeader(result.Data.CurrentPage, result.Data.PageSize, result.Data.TotalCount, result.Data.TotalPages));

            // Return the result wrapped in an ApiResponse object
            return Ok(result);
        }

    
       

    }
}

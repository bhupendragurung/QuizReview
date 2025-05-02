using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizReviewApplication.WebApi.Services;

namespace QuizReviewApplication.WebApi.Controllers.ChatGpt
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly OpenAiServices _openAiServices;
        public ChatController(OpenAiServices openAiServices)
        {
                _openAiServices = openAiServices;
        }
        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] string prompt)
        {
            if (string.IsNullOrEmpty(prompt))
            {
                return BadRequest("Prompt cannot be empty.");
            }

            try
            {
                var response = await _openAiServices.GetResponseFromDeepSeek();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

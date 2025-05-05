using QuizReviewApplication.Domain.Entities;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using QuizReviewApplication.Application.Services;
using System.Text.RegularExpressions;

namespace QuizReviewApplication.WebApi.Services
{
    public class OpenAiServices:IAiServices
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;
       
        public OpenAiServices(HttpClient http,IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        public async Task<string> GetResponseFromAi(string prompt)
        {

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_config["DeepSeek:ApiKey"]}";

            var requestBody = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(content);

                var responseText = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();
                string cleanJson = Regex.Match(responseText, @"\{[\s\S]*\}").Value;
                return cleanJson;
            }
            else
            {
                throw new Exception($"Error calling Gemini API: {response.StatusCode}");
            }
        }

        public async Task<string> GetResponseFromDeepSeek()
        {
            var prompt = @"
Evaluate the following student's answer to a question using this rubric:

Rubric:
0 = Incorrect or no answer.
1 = Partially correct but missing key elements.
2 = Mostly correct, minor issues.
3 = Fully correct and well-explained.

Question: what is c#
Student Answer: c# is object oriented programming language and develoeped by microsoft

Provide only a JSON response like:
{ ""score"": [0-3], ""feedback"": ""Your explanation here"" }
";

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_config["DeepSeek:ApiKey"]}";

            var requestBody = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(content);

                var responseText = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return responseText;
            }
            else
            {
                throw new Exception($"Error calling Gemini API: {response.StatusCode}");
            }
        }
        public async Task<string> GetResponseFromOpenAi()
    {
            var prompt = $@"
Evaluate the following student's answer to a question using this rubric:

Rubric:
0 = Incorrect or no answer.
1 = Partially correct but missing key elements.
2 = Mostly correct, minor issues.
3 = Fully correct and well-explained.

Question: what is c#
Student Answer: c# is object oriented programming language and develoeped by microsoft

Provide only a JSON response like:
{{ ""score"": [0-3], ""feedback"": ""Your explanation here"" }}
";

            var requestBody = new
            {
                model = "gpt-4.1",
                messages = new[]
                {
            new { role = "system", content = "You are an expert examiner scoring student answers." },
            new { role = "user", content = prompt }
        },
                temperature = 0.2
            };
            var requestJson = JsonSerializer.Serialize(requestBody);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _config["OpenAI:ApiKey"]);
            request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

                var response = await _http.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(content);
                    var responseText = doc.RootElement
                        .GetProperty("choices")[0]
                        .GetProperty("message")
                        .GetProperty("content")
                        .GetString();

             return responseText;
                }
                else
            {
                    throw new Exception($"Error: {response.StatusCode}");
            }
                
              
            

        
    }
    }
}

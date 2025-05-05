using AutoMapper;
using FluentValidation;
using MediatR;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Application.Repositories;
using QuizReviewApplication.Application.Services;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.QuestionAnswers.Commands.CreateQuestionAnswer
{
    public class CreateQuestionAnswerHandler : IRequestHandler<CreateQuestionAnswerCommand, ApiResponse<string>>
    {
        private readonly IAnswerRepository _questionAnswerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAiServices _aiServices;
        private readonly IMapper _mapper;

        public CreateQuestionAnswerHandler(IAnswerRepository questionAnswerRepository, IQuestionRepository questionRepository, IAiServices aiServices, IMapper mapper)
        {
            _questionAnswerRepository = questionAnswerRepository;
            _questionRepository = questionRepository;
            _aiServices = aiServices;
            _mapper = mapper;
        }
        public async Task<ApiResponse<string>> Handle(CreateQuestionAnswerCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetQuestionByIdAsync(request.QuestionId);
            if (question == null)
            {
                return ApiResponse<string>.FailureResponse("Question not found.");
            }
            var questionAnswer = _mapper.Map<Answer>(request);
            var evaluationResponseDto = await GetEvaluationResponseDto(request.QuestionId, request.Text);
            if (evaluationResponseDto != null)
            {
                questionAnswer.Marks = evaluationResponseDto.Score;
                questionAnswer.Feedback = evaluationResponseDto.Feedback;
            }
            else
            {
                questionAnswer.Marks = 0;
                questionAnswer.Feedback = "No feedback provided";
            }

            var result = await _questionAnswerRepository.CreateAsync(questionAnswer);
            return result != null
                ? ApiResponse<string>.SuccessResponse("Answer created successfully.", result.Id.ToString())
                : ApiResponse<string>.FailureResponse("Failed to create answer.");

            

        }

        public async Task<EvaluationResponseDto> GetEvaluationResponseDto(Guid questionId, string answer)
        {
            try
            {

                var result = await _questionRepository.GetQuestionByIdAsync(questionId);
                if (result != null)
                {
                    var prompt = $@"
                        Evaluate the following student's answer to a question using this rubric:

                        Rubric:
                        0 = Incorrect or no answer.
                        1 = Partially correct but missing key elements.
                        2 = Mostly correct, minor issues.
                        3 = Fully correct and well-explained.

                        Question:{result.Text} 
                         Answer: {answer}
                       Provide only a JSON response like:
                        {{ ""Score"": [0-3], ""Feedback"": ""Your explanation here"" }}

                        Respond with ONLY a valid JSON object. Do not include explanations or extra characters. Just return: {{ ""Score"": x, ""Feedback"": ""..."" }}.
                        ";
                    var response = await _aiServices.GetResponseFromAi(prompt);
                    var evaluatedResponse = JsonSerializer.Deserialize<EvaluationResponseDto>(response);

                    return evaluatedResponse;
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}

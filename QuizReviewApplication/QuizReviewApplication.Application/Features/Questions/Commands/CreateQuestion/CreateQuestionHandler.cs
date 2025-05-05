using AutoMapper;
using MediatR;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Application.Repositories;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionHandler : IRequestHandler<CreateQuestionCommand, ApiResponse<Guid>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IQuestionCategoryRepository _questionCategoryRepository;
       

        public CreateQuestionHandler(IQuestionRepository questionRepository, ICategoryRepository categoryRepository, IQuestionCategoryRepository questionCategoryRepository)
        {
            _questionRepository = questionRepository;
            _categoryRepository = categoryRepository;
            _questionCategoryRepository = questionCategoryRepository;
       
        }

        public async Task<ApiResponse<Guid>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if question already exists
                var existingQuestion = await _questionRepository.GetQuestionByContentAsync(request.Content);
                Guid questionId;

                if (existingQuestion != null)
                {
                    questionId = existingQuestion.Id;
                }
                else
                {
                    var question = new Question
                    {
                        Id = Guid.NewGuid(),
                        Text = request.Content,
                        SkillLevel = (SkillType)request.SkillLevel,
                        QuestionLevel = (QuestionType)request.QuestionLevel
                    };

                    var createdQuestion = await _questionRepository.CreateAsync(question);
                    if (createdQuestion == null)
                    {
                        return ApiResponse<Guid>.FailureResponse("Failed to create question.");
                    }

                    questionId = createdQuestion.Id;
                }

                // Ensure category is linked
                var categoryLinked = await _questionCategoryRepository.CheckQuestionCategoryExists(questionId, request.CategoryId);
                if (!categoryLinked)
                {
                    await _questionCategoryRepository.AddQuestionCategory(questionId, request.CategoryId);
                }

                return ApiResponse<Guid>.SuccessResponse("Question created successfully.", questionId);
            }
            catch (Exception ex)
            {
                return ApiResponse<Guid>.FailureResponse("An error occurred while creating the question.");
            }

        }
    }
}

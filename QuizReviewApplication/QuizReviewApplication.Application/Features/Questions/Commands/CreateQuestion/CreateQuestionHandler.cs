using AutoMapper;
using MediatR;
using QuizReviewApplication.Application.Dtos;
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
    public class CreateQuestionHandler : IRequestHandler<CreateQuestionCommand, CreateQuestionResponse>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IQuestionCategoryRepository _questionCategoryRepository;
        private readonly IMapper _mapper;

        public CreateQuestionHandler(IQuestionRepository questionRepository, ICategoryRepository categoryRepository, IQuestionCategoryRepository questionCategoryRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _categoryRepository = categoryRepository;
            _questionCategoryRepository = questionCategoryRepository;
            _mapper = mapper;
        }

        public async Task<CreateQuestionResponse> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var questionResponse= new CreateQuestionResponse();
            var validator= new CreateQuestionValidator();
            try
            {
                Guid questionId = Guid.NewGuid();
               
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (validationResult.IsValid)
                {
                    questionResponse.Success = true;
                    //check Question is exist or not 
                    if (await _questionRepository.CheckQuestionExists(request.Content))
                    {
                        var questionBySearchNameResult = await _questionRepository.GetQuestionByContentAsync(request.Content);
                        if (questionBySearchNameResult != null)
                        {
                            questionId = questionBySearchNameResult.Id;
                            questionResponse.QuestionId= questionId;
                        }

                    }
                    //if not exist enter to the database
                    else
                    {
                        var question = new Question();
                        question.Text = request.Content;
                        question.SkillLevel = (SkillType)request.SkillLevel;
                        question.QuestionLevel = (QuestionType)request.QuestionLevel;

                        var result = await _questionRepository.CreateAsync(question);
                        if (result != null)
                        {
                            questionId = result.Id;
                            questionResponse.QuestionId = questionId;
                        }
                    }
                    if (!await _questionCategoryRepository.CheckQuestionCategoryExists(questionId, request.CategoryId))
                    {
                        await _questionCategoryRepository.AddQuestionCategory(questionId, request.CategoryId);
                    }


                }
                else
                {
                    questionResponse.Success = false;
                    if (validationResult.Errors.Count > 0)
                    {
                        questionResponse.ValidationErrors = new List<string>();
                        foreach (var error in validationResult.Errors.Select(e => e.ErrorMessage))
                        {
                            questionResponse.ValidationErrors.Add(error);
                        }
                    }
                }
            }
            catch (Exception)
            {

                questionResponse.Success = false;
                questionResponse.Message = "Server Error";
            }

            return questionResponse;

        }
    }
}

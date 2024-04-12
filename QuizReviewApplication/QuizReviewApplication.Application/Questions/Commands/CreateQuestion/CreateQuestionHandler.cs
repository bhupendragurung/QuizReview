using AutoMapper;
using MediatR;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Domain.Enum;
using QuizReviewApplication.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Questions.Commands.CreateQuestion
{
    public class CreateQuestionHandler : IRequestHandler<CreateQuestionCommand, QuestionDto>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IQuestionCategoryRepository _questionCategoryRepository;
        private readonly IMapper _mapper;

        public CreateQuestionHandler(IQuestionRepository questionRepository,ICategoryRepository categoryRepository,IQuestionCategoryRepository questionCategoryRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _categoryRepository = categoryRepository;
            _questionCategoryRepository = questionCategoryRepository;
            _mapper = mapper;
        }
        public async Task<QuestionDto> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
          // category data will be added and return id
          // if category already exists then it will return only id

            var categoryId = await _categoryRepository.CreateCategoryAsync(request.CategoryName, request.CategoryValue);

            // question data will be added and return question
            var question = new Question();
            question.Text = request.Content;
            question.SkillLevel = (SkillType)request.SkillLevel;
            question.QuestionLevel = (QuestionType)request.QuestionLevel;

            var result= await _questionRepository.CreateAsync(question);

            // questioncategory data will be added and return nothing
            _questionCategoryRepository.AddQuestionCategory(question.Id, categoryId);
            return _mapper.Map<QuestionDto>(result);
            
        }
    }
}

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
            Guid questionId= Guid.NewGuid();
            var question = new Question();
            //check Question is exist or not 
            if ( await _questionRepository.CheckQuestionExists(request.Content))
            {
                var quest=await _questionRepository.GetQuestionByContentAsync(request.Content);
                if(quest != null)
                {
                    question = quest;
                    questionId = quest.Id;
                }
               
            }
            else
            {
                
                question.Text = request.Content;
                question.SkillLevel = (SkillType)request.SkillLevel;
                question.QuestionLevel = (QuestionType)request.QuestionLevel;

                var result = await _questionRepository.CreateAsync(question);
                if(result != null)
                {
                    questionId = result.Id;
                    question = result;
                }
            }
            if (!await _questionCategoryRepository.CheckQuestionCategoryExists(questionId, request.CategoryId))
            {
              await  _questionCategoryRepository.AddQuestionCategory(questionId, request.CategoryId);
            }
           
            return _mapper.Map<QuestionDto>(question);
            
        }
    }
}

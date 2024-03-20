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
        private readonly IMapper _mapper;

        public CreateQuestionHandler(IQuestionRepository questionRepository,IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public async Task<QuestionDto> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = new Question() { Text = request.Content, SkillLevel = (SkillType)request.SkillLevel, QuestionLevel = (QuestionType)request.QuestionLevel, Category = request.Category };
            var result= await _questionRepository.CreateAsync(question);
            return _mapper.Map<QuestionDto>(result);
            
        }
    }
}

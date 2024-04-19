using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Questions.Queries.GetQuestions
{
    public class GetQuestionsHandler : MediatR.IRequestHandler<GetQuestionsQuery, IEnumerable<QuestionDto>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public GetQuestionsHandler(IQuestionRepository questionRepository,IMapper mapper )
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<QuestionDto>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            var questions= await _questionRepository.GetAllQuestionsAsync();
         
            if(questions != null ) {
               
                return questions.Select(ques => _mapper.Map<QuestionDto>(ques)).ToList();
            }
            
            return new List<QuestionDto>();
           
        }
    }
}

using AutoMapper;
using MediatR;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Questions.Queries.GetQuestions
{
    public class GetQuestionsHandler : IRequestHandler<GetQuestionsQuery, List<QuestionDto>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public GetQuestionsHandler(IQuestionRepository questionRepository,IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public async Task<List<QuestionDto>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            var questions= await _questionRepository.GetAllQuestions();
            return questions.Select(ques => _mapper.Map<QuestionDto>(ques)).ToList();
           
        }
    }
}

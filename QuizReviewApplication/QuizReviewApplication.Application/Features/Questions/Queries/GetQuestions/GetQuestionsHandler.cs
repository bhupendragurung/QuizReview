using AutoMapper;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Questions.Queries.GetQuestions
{
    public class GetQuestionsHandler : MediatR.IRequestHandler<GetQuestionsQuery, GetQuestionsResponse>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public GetQuestionsHandler(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public async Task<GetQuestionsResponse> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            var getQuestionnResponse = new GetQuestionsResponse();
            try
            {
                var questions = await _questionRepository.GetAllQuestionsAsync();

                if (questions != null)
                {

                    getQuestionnResponse.Questions = _mapper.Map<List<QuestionDto>>(questions);
                }

                
            }
            catch (Exception ex)
            {
                getQuestionnResponse.Success = false;
                getQuestionnResponse.Message = "Server Error";
            }

            return getQuestionnResponse;
        }
    }
}

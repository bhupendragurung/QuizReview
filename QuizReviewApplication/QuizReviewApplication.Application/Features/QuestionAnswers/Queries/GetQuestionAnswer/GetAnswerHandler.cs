using AutoMapper;
using MediatR;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.QuestionAnswers.Queries.GetQuestionAnswer
{
    public class GetAnswerHandler : IRequestHandler<GetAnswerQuery, ApiResponse<List<AnswerDto>>>
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public GetAnswerHandler(IAnswerRepository answerRepository , IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }


        public async Task<ApiResponse<List<AnswerDto>>> Handle(GetAnswerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _answerRepository.GetAllQuestionAnswerAsync();
                var mappedResult = _mapper.Map<List<AnswerDto>>(result);
                return ApiResponse<List<AnswerDto>>.SuccessResponse("All Question Fetehced Successfully", mappedResult);
            }
            catch
            {
                return ApiResponse<List<AnswerDto>>.FailureResponse("Failed to retrieve answers.");
            }
        }
    }
}

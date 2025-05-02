using AutoMapper;
using MediatR;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.Questions.Queries.GetQuestions;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Questions.Queries.GetQuestionById
{
    public class GetQuestionByIdHandler : IRequestHandler<GetQuestionByIdQuery, ApiResponse<QuestionDto>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public GetQuestionByIdHandler(IQuestionRepository questionRepository,IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<QuestionDto>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
         
            try
            {
              
                    var question = await _questionRepository.GetQuestionByIdAsync(request.QuestionId);
                if (question == null)
                {
                    return ApiResponse<QuestionDto>.FailureResponse("Question not found.");
                }
               var questionDto = _mapper.Map<QuestionDto>(question);
                questionDto.CategoryId = question.QuestionCategories.FirstOrDefault()?.CategoryId ?? Guid.Empty;
                return ApiResponse<QuestionDto>.SuccessResponse("Question Feteched Successfully", questionDto);

            }
            catch (Exception)
            {

                return ApiResponse<QuestionDto>.FailureResponse("An error occurred while fetching the question.");
            }

        }
    }
}

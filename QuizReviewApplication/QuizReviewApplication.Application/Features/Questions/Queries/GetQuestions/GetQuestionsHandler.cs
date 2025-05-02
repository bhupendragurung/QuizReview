using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Questions.Queries.GetQuestions
{
    public class GetQuestionsHandler : MediatR.IRequestHandler<GetQuestionsQuery,ApiResponse< PagedList<QuestionDto>>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public GetQuestionsHandler(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<PagedList<QuestionDto>>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var pagedQuestions = await _questionRepository.GetAllQuestionsAsync(request);

                return ApiResponse<PagedList<QuestionDto>>.SuccessResponse("All Question Fetehced Successfully",pagedQuestions);
            }
            catch (Exception ex)
            {
                // Optional: log exception
                return ApiResponse<PagedList<QuestionDto>>.FailureResponse("Failed to retrieve questions.");
            }

        }
    }
}

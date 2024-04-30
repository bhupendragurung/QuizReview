using AutoMapper;
using MediatR;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Features.Questions.Queries.GetQuestions;
using QuizReviewApplication.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Questions.Queries.GetQuestionById
{
    public class GetQuestionByIdHandler : IRequestHandler<GetQuestionByIdQuery, GetQuestionByIdResponse>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public GetQuestionByIdHandler(IQuestionRepository questionRepository,IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public async Task<GetQuestionByIdResponse> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
          var questionResponse= new GetQuestionByIdResponse();
            var validator= new GetQuestionByIdValidator();
            try
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if(validationResult.IsValid )
                {
                  questionResponse.Success = true;
                    var result = await _questionRepository.GetQuestionByIdAsync(request.QuestionId);
                    questionResponse.Question = _mapper.Map<QuestionDto>(result);

                }
                else {
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

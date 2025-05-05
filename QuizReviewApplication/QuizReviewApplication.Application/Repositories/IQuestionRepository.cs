using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Repositories
{
    public interface IQuestionRepository
    {
        Task<PagedList<QuestionDto>> GetAllQuestionsAsync(QuestionParams questionParams);
        Task<bool> CheckQuestionExists(string content);
        Task<Question> GetQuestionByContentAsync(string content);
        Task<Question> GetQuestionByIdAsync(Guid Id);
        Task<Question> CreateAsync(Question question);
    }
}

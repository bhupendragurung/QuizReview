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
        Task<List<Question>> GetAllQuestionsAsync();
        Task<bool> CheckQuestionExists(string content);
        Task<Question> GetQuestionByContentAsync(string content);
        Task<Question> GetQuestionByIdAsync(Guid Id);
        Task<Question> CreateAsync(Question question);
    }
}

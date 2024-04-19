using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Domain.Repositories
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetAllQuestionsAsync();
        Task<bool> CheckQuestionExists(string content);
        Task<Question> GetQuestionByContentAsync(string content);
        Task<Question> CreateAsync(Question question);
    }
}

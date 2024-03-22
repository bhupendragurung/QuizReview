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
        Task<List<Question>> GetAllQuestions();
        Task<Question> CreateAsync(Question question);
    }
}

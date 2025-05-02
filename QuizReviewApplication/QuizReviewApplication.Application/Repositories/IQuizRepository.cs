using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Repositories
{
    public interface IQuizRepository
    {
        Task<List<Quiz>> GetAllQuizAsync();
        Task<Quiz> GetQuizByIdAsync(Guid Id);
        Task<Quiz> CreateAsync(Quiz quiz);

    }
}

using Microsoft.EntityFrameworkCore;
using QuizReviewApplication.Application.Repositories;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Infrastructure.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly QuizReviewDbContext _quizReviewDbContext;

        public QuizRepository(QuizReviewDbContext quizReviewDbContext)
        {
            _quizReviewDbContext = quizReviewDbContext;
        }
        public async Task<Quiz> CreateAsync(Quiz quiz)
        {
            await _quizReviewDbContext.Quizzes.AddAsync(quiz);
            await _quizReviewDbContext.SaveChangesAsync();
            return quiz;
        }

        public async Task<List<Quiz>> GetAllQuizAsync()
        {
          return await  _quizReviewDbContext.Quizzes.ToListAsync();
        }

        public async Task<Quiz> GetQuizByIdAsync(Guid Id)
        {
            return await _quizReviewDbContext.Quizzes.FirstOrDefaultAsync(q=>q.Id==Id);

        }
    }
}

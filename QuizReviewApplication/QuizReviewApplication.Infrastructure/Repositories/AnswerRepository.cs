using Microsoft.EntityFrameworkCore;
using QuizReviewApplication.Application.Repositories;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Infrastructure.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly QuizReviewDbContext _quizReviewDbContext;

        public AnswerRepository(QuizReviewDbContext quizReviewDbContext)
        {
            _quizReviewDbContext = quizReviewDbContext;
        }
        public async Task<Answer> CreateAsync(Answer questionAnswer)
        {
            await _quizReviewDbContext.Answers.AddAsync(questionAnswer);
            await _quizReviewDbContext.SaveChangesAsync();
            return questionAnswer;
        }

        public async Task<List<Answer>> GetAllQuestionAnswerAsync()
        {
           return  await _quizReviewDbContext.Answers.
                Include(q=>q.Question)
                .ToListAsync();
        }

        public async Task<Answer> GetQuestionAnswerByIdAsync(Guid Id)
        {
            return await _quizReviewDbContext.Answers.FirstOrDefaultAsync(q=>q.Id==Id);

        }
    }
}

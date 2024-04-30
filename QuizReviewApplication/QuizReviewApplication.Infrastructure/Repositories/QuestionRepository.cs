using Microsoft.EntityFrameworkCore;
using QuizReviewApplication.Application.Repositories;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Infrastructure.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuizReviewDbContext _quizReviewDbContext;

        public QuestionRepository(QuizReviewDbContext quizReviewDbContext)
        {
            _quizReviewDbContext = quizReviewDbContext;
        }

        public async Task<bool> CheckQuestionExists(string content)
        {
           return await _quizReviewDbContext.Questions.AnyAsync(q=>q.Text.ToLower() == content.ToLower());
        }
        public async Task<Question> CreateAsync(Question question)
        {
            await _quizReviewDbContext.Questions.AddAsync(question);
            await _quizReviewDbContext.SaveChangesAsync();
            return question;
        }
        
        public async Task<List<Question>> GetAllQuestionsAsync()
        {        
              
            var questions= await  _quizReviewDbContext.Questions
                .Include(qc=>qc.QuestionCategories)
                .ThenInclude(c=>c.Category)
                .ToListAsync();
            return questions;
        }

        public async Task<Question> GetQuestionByContentAsync(string content)
        {          
                var question= await _quizReviewDbContext.Questions
                   .Include(qc => qc.QuestionCategories)
                   .ThenInclude(c => c.Category).FirstOrDefaultAsync(q => q.Text.ToLower() == content.ToLower());
                return question;
        }

        public async Task<Question> GetQuestionByIdAsync(Guid Id)
        {
            return await _quizReviewDbContext.Questions
                  .Include(qc => qc.QuestionCategories)
                  .ThenInclude(c => c.Category).FirstOrDefaultAsync(q => q.Id == Id);
        
        }
    }
}

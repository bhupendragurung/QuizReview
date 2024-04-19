using Microsoft.EntityFrameworkCore;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Domain.Repositories;
using QuizReviewApplication.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Infrastructure.Repositories
{
    public class QuestionCategroyRepository : IQuestionCategoryRepository
    {
        private readonly QuizReviewDbContext _quizReviewDbContext;
        public QuestionCategroyRepository(QuizReviewDbContext quizReviewDbContext)
        {
            _quizReviewDbContext = quizReviewDbContext;
        }
        public async Task<bool> CheckQuestionCategoryExists(Guid questionId, Guid categroyId)
        {
            return await _quizReviewDbContext.QuestionCategories.AnyAsync(c => c.QuestionId == questionId && c.CategoryId == categroyId);
        }
        public async Task AddQuestionCategory(Guid questionId, Guid categroyId)
        {
                await _quizReviewDbContext.QuestionCategories.AddAsync(new QuestionCategory { QuestionId = questionId, CategoryId = categroyId });
               await _quizReviewDbContext.SaveChangesAsync();
            
           
        }
    }
}

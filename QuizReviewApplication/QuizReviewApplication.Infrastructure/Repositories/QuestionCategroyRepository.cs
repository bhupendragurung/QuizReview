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
        public void AddQuestionCategory(Guid questionId, Guid categroyId)
        {
            _quizReviewDbContext.QuestionCategories.Add(new QuestionCategory { QuestionId=questionId,CategoryId=categroyId});
            _quizReviewDbContext.SaveChanges();
        }
    }
}

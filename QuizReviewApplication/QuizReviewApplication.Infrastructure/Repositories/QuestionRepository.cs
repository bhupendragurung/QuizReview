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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuizReviewDbContext _quizReviewDbContext;

        public QuestionRepository(QuizReviewDbContext quizReviewDbContext)
        {
            _quizReviewDbContext = quizReviewDbContext;
        }

        public async Task<Question> CreateAsync(Question question)
        {
             _quizReviewDbContext.Questions.Add(question);
             _quizReviewDbContext.SaveChanges();
            return question;
        }

        public async Task<List<Question>> GetAllQuestions()
        {        
              
            return  _quizReviewDbContext.Questions.ToList();

        }
    }
}

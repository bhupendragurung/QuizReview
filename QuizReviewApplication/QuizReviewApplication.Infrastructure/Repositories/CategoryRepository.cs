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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly QuizReviewDbContext _quizReviewDbContext;

        public CategoryRepository(QuizReviewDbContext quizReviewDbContext)
        {
            _quizReviewDbContext = quizReviewDbContext;
        }

        public async Task<Guid> CreateCategoryAsync(string categoryName, string categoryDescription)
        {
            var existingCategory = await GetCategoryByName(categoryName);
            if (existingCategory != null)
            {
                return existingCategory.Id;
            }
            var newCategory=new Category { Name = categoryName, Description = categoryDescription };
           await _quizReviewDbContext.Categories.AddAsync(newCategory);
            await _quizReviewDbContext.SaveChangesAsync();
            return newCategory.Id;
        }

        public  Task<Category> GetCategoryByName(string categoryName="")
        {
            return Task.FromResult(_quizReviewDbContext.Categories.FirstOrDefault(c => c.Name == categoryName));
        }
    }
}

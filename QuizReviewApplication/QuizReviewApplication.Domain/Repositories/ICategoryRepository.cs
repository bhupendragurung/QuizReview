using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Guid> CreateCategoryAsync(string categoryName,string categoryDescription );
        Task<Category> GetCategoryByName(string categoryName);
    }
}

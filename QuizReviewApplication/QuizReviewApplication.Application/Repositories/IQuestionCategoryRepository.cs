using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Repositories
{
    public interface IQuestionCategoryRepository
    {
        Task AddQuestionCategory(Guid questionId, Guid categroyId);
        Task<bool> CheckQuestionCategoryExists(Guid questionId, Guid categroyId);
    }
}

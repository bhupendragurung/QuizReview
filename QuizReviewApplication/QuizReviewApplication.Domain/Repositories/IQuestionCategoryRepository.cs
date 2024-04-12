using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Domain.Repositories
{
    public interface IQuestionCategoryRepository
    {
        void AddQuestionCategory(Guid questionId, Guid categroyId);
    }
}

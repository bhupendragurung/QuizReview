using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Repositories
{
    public interface IAnswerRepository
    {
        Task<List<Answer>> GetAllQuestionAnswerAsync();
        Task<Answer> GetQuestionAnswerByIdAsync(Guid Id);
        Task<Answer> CreateAsync(Answer answer);
    }
}

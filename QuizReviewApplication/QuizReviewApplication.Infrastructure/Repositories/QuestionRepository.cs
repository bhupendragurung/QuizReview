using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Application.Repositories;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Domain.Enum;
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
        private readonly IMapper _mapper;
        public QuestionRepository(QuizReviewDbContext quizReviewDbContext , IMapper mapper)
        {
            _quizReviewDbContext = quizReviewDbContext;
            _mapper = mapper;
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
        
        public async Task<PagedList<QuestionDto>> GetAllQuestionsAsync(QuestionParams questionParams)
        {        
              
            var query=   _quizReviewDbContext.Questions
                .Include(qc=>qc.QuestionCategories)
                .ThenInclude(c=>c.Category)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(questionParams.Search))
            {
                query = query.Where(q => q.Text.ToLower().Contains(questionParams.Search.ToLower()));

            }
            if (!string.IsNullOrWhiteSpace(questionParams.QuestionLevel))
            {
                if (Enum.TryParse<QuestionType>(questionParams.QuestionLevel, out var parsedLevel))
                {
                    query = query.Where(q => q.QuestionLevel == parsedLevel);
                }
            }
            if (!string.IsNullOrWhiteSpace(questionParams.SkillLevel))
            {
                if (Enum.TryParse<SkillType>(questionParams.SkillLevel, out var parsedLevel))
                {
                    query = query.Where(q => q.SkillLevel == parsedLevel);
                }
            }
          

            return await PagedList<QuestionDto>.CreateAsync(query.AsNoTracking().ProjectTo<QuestionDto>(_mapper.ConfigurationProvider),questionParams.PageNumber,questionParams.PageSize);
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

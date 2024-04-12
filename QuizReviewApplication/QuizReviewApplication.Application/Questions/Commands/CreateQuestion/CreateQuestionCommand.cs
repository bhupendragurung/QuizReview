using MediatR;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommand:IRequest<QuestionDto>
    {
        public string Content { get; set; } = string.Empty;
        public int SkillLevel { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryValue { get; set; } = string.Empty;
        public int QuestionLevel { get; set; }
    }
}

using MediatR;
using QuizReviewApplication.Application.Dtos;
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
        public String Category { get; set; }
        public int QuestionLevel { get; set; }
    }
}

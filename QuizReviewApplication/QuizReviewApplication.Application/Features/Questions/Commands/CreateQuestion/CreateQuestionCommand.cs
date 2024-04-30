using MediatR;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommand : IRequest<CreateQuestionResponse>
    {
        public string Content { get; set; } = string.Empty;
        public int SkillLevel { get; set; }
        public Guid CategoryId { get; set; }
        public int QuestionLevel { get; set; }
    }
}

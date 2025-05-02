using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Dtos
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Content { get; set; } = string.Empty;
        public int SkillLevel { get; set; }
        public string  Category { get; set; } = string.Empty;
        public int QuestionLevel { get; set; }
    }
}

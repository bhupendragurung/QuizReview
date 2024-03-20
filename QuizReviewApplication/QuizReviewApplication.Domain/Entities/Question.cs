using QuizReviewApplication.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Domain.Entities
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Text { get; set; }=string.Empty;
        public SkillType SkillLevel { get; set; }
        public String Category { get; set; }
        public QuestionType QuestionLevel { get; set; }
    }
}

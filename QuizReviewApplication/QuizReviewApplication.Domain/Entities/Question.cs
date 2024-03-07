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
        public SkillLevel? skillLevel { get; set; }
        public Category? category { get; set; }
        public QuestionLevel? questionLevel { get; set; }
    }
}

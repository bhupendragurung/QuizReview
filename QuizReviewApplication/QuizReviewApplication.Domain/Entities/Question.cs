using QuizReviewApplication.Domain.Enum;
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
        // Difficulty level change the property name
        public QuestionType QuestionLevel { get; set; }
        public IList<QuestionCategory> QuestionCategories { get; set; }
    }
}

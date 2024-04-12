using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Domain.Entities
{
    public class QuestionCategory
    {
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

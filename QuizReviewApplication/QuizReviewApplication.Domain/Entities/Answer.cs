using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Domain.Entities
{
    public class Answer
    {
        public Guid Id { get; set; }
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public string Feedback { get; set; }
        public int Marks { get; set; }


    }
}

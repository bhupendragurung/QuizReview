using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Domain.Entities
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public string QuizTitle { get; set; }
        public string UserName { get; set; }
        public string CheckBy { get; set; }
        public int TotalMarks { get; set; }
    }
}

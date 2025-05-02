using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Dtos
{
    public class AnswerDto
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Feedback { get; set; } = string.Empty;
        public int Score { get; set; } 
    }
}

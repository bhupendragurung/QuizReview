using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Helper
{
    public class QuestionParams:PaginationParams
    {
        public string Search { get; set; } = "";
        public string QuestionLevel { get; set; } = "";
        public string SkillLevel { get; set; } = "";
        public string Category { get; set; } = "";
    }
}

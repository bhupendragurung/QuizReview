using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Domain.Entities
{
    //Question is divided into different level
    // Easy, Medium and Hard
    public class QuestionLevel
    {
        public int Id { get; set; }
        public string Level { get; set; } = "Easy";
        public int QuestionId {get;set;}    
        public Question Questions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Domain.Enitities
{
    // user can have different level 
    // some may be entry level , mid level and professional
    // question can be varied depending upon user
    public class SkillLevel
    {
        public int Id { get; set; }
        public string Skill { get; set; } = "EntryLevel";
        public ICollection<Question> Questions { get; set; }
    }
}

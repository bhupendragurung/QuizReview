using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Domain.Entities
{
    // Each Question belong to one or more category 
    // For example one question belong to c# and .Net Category
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public ICollection<Question> Questions { get; set; }
    }
}

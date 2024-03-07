using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Infrastructure.Data
{
    public class QuizReviewDbContext:DbContext
    {
        public QuizReviewDbContext(DbContextOptions<QuizReviewDbContext> options):base(options)
        {
                
        }

    }
}

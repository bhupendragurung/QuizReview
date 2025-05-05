using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuizReviewApplication.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuizReviewApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace QuizReviewApplication.Infrastructure.Data
{
    public class QuizReviewDbContext : IdentityDbContext<ApplicationUser>
    {
      
      
        public QuizReviewDbContext(DbContextOptions<QuizReviewDbContext> options):base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<QuestionCategory>().HasKey(qc => new { qc.QuestionId, qc.CategoryId });
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        
    }
}

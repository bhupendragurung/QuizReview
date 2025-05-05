using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizReviewApplication.Application.Interfaces;
using QuizReviewApplication.Application.Repositories;
using QuizReviewApplication.Application.Services;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Infrastructure.Data;
using QuizReviewApplication.Infrastructure.Repositories;
using QuizReviewApplication.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Infrastructure.Extensions
{
    public static class InfrastructureDIRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QuizReviewDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresDatabase"));
            }, ServiceLifetime.Scoped);
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 1;
                options.Lockout.AllowedForNewUsers = true;

            })
    .AddEntityFrameworkStores<QuizReviewDbContext>()
    .AddDefaultTokenProviders();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISignInService, SignInService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IQuestionCategoryRepository, QuestionCategroyRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();
            return services;
        }
    }
}

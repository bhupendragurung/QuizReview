using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizReviewApplication.Application.Repositories;
using QuizReviewApplication.Application.Services;
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

            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IQuestionCategoryRepository, QuestionCategroyRepository>();
            return services;
        }
    }
}

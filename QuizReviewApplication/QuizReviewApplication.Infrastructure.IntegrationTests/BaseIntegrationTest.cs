using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QuizReviewApplication.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.IntegrationTests
{
    public abstract class BaseIntegrationTest:IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _serviceScope;
        protected readonly ISender _sender;
        protected readonly QuizReviewDbContext _quizReviewDbContext;

       

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _serviceScope = factory.Services.CreateScope();
            _sender= _serviceScope.ServiceProvider.GetRequiredService<ISender>();
            _quizReviewDbContext = _serviceScope.ServiceProvider.GetRequiredService<QuizReviewDbContext>();
        }
    }
}

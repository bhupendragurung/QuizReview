using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizReviewApplication.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

namespace QuizReviewApplication.Application.IntegrationTests
{
    public class IntegrationTestWebAppFactory:WebApplicationFactory<Program>,IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer= new PostgreSqlBuilder().
            WithImage("postgres:latest")
            .WithDatabase("postgrestest")
            .WithUsername("postgres")
            .WithPassword("mysecretpassword")
            .Build();

        public Task InitializeAsync()
        {
            return _dbContainer.StartAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(s =>
                
                    s.ServiceType == typeof(DbContextOptions<QuizReviewDbContext>)
                );
                if ( descriptor is not null )
                {
                    services.Remove( descriptor );
                }

                services.AddDbContext<QuizReviewDbContext>(options =>
                {
                    options.UseNpgsql(_dbContainer.GetConnectionString());
                });

            });
        }

        Task IAsyncLifetime.DisposeAsync()
        {
            return _dbContainer.StopAsync();
        }
    }
}

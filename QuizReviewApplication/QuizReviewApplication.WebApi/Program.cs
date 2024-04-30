using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuizReviewApplication.Application.Extensions;
using QuizReviewApplication.Infrastructure.Data;
using QuizReviewApplication.Infrastructure.Extensions;
using QuizReviewApplication.Infrastructure.Repositories;
using QuizReviewApplication.WebApi.Extensions;
using QuizReviewApplication.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// add layar dependecny 
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.TokenAuthentication(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
//builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
//app.UseExceptionHandler();
app.MapControllers();

app.Run();

public partial class Program { }
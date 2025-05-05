using Asp.Versioning;
using CorrelationId;
using CorrelationId.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuizReviewApplication.Application.Extensions;
using QuizReviewApplication.Application.Services;
using QuizReviewApplication.Infrastructure.Data;
using QuizReviewApplication.Infrastructure.Extensions;
using QuizReviewApplication.Infrastructure.Repositories;
using QuizReviewApplication.WebApi.Extensions;
using QuizReviewApplication.WebApi.Middleware;
using QuizReviewApplication.WebApi.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithCorrelationId()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
//     builder.Services.AddHttpClient<OpenAiServices>(client =>
//     {
//         client.BaseAddress = new Uri("https://api.deepseek.com/v1/");
//     });

//builder.Services.AddSingleton(sp =>
//         new OpenAiServices(
//             sp.GetRequiredService<IHttpClientFactory>().CreateClient(),builder.Configuration.GetSection("DeepSeek:ApiKey")));

// Add services to the container.
// add layar dependecny
builder.Services.AddDefaultCorrelationId(options =>
{
    options.AddToLoggingScope = true; // Add CorrelationId into Serilog context automatically
    options.UpdateTraceIdentifier = true;
    options.RequestHeader = "X-Correlation-ID"; // Optional: allow clients to pass their own ID
    options.ResponseHeader = "X-Correlation-ID";
});
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.TokenAuthentication(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmailService, SmtpEmailService>();
builder.Services.AddScoped<IAiServices, OpenAiServices>();

builder.Services.AddHttpClient<OpenAiServices>();
//builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
//builder.Services.AddProblemDetails();

var apiVersioningBuilder = builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});
apiVersioningBuilder.AddApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandler>();
app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseCorrelationId();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
//app.UseExceptionHandler();
app.MapControllers();

app.Run();

public partial class Program { }
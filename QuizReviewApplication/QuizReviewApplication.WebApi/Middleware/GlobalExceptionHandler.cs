using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QuizReviewApplication.Application.Helper;
using System.Net;
using System.Text.Json;

namespace QuizReviewApplication.WebApi.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly RequestDelegate _next;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception ex)
            {
                await HandleGenericExceptionAsync(context, ex);
            }
        }



        private static Task HandleGenericExceptionAsync(HttpContext context, Exception exception)
        {
            ApiResponse<object> apiResponse;
            context.Response.ContentType = "application/json";
            if (exception is ValidationException validationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var errorList = validationException.Errors
          .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
          .ToList();
                apiResponse = ApiResponse<object>.FailureResponse("Validation Failed", errorList);


                return context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse));
            }
            else if (exception is KeyNotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                apiResponse = ApiResponse<object>.FailureResponse("Resource not found.", new List<string> { exception.Message });


                return context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse));
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                apiResponse = ApiResponse<object>.FailureResponse("An unexpected error occurred. Please try again later.", new List<string> { "Internal Server Error" });


                return context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse));
            }


        }


    }
}

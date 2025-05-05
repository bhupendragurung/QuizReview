using System.Text;

namespace QuizReviewApplication.WebApi.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {// Skip health check endpoints
            if (context.Request.Path.StartsWithSegments("/health"))
            {
                await _next(context);
                return;
            }
            // Log request metadata
            _logger.LogInformation("REQUEST: {Method} {Path} {Query}",
                context.Request.Method,
                context.Request.Path,
                context.Request.QueryString);

            // Only capture response body if it's text-based (for efficiency)
            var originalBodyStream = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            await _next(context); // Call the next middleware / controller

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            // Optionally limit large bodies or filter content types
            if (IsTextBasedContentType(context.Response.ContentType))
            {
                _logger.LogInformation("RESPONSE: {StatusCode} | Body: {Body}",
                    context.Response.StatusCode,
                    Truncate(responseBody, 1000));
            }
            else
            {
                _logger.LogInformation("RESPONSE: {StatusCode} | Non-text content",
                    context.Response.StatusCode);
            }

            await responseBodyStream.CopyToAsync(originalBodyStream);
        }

        private static bool IsTextBasedContentType(string? contentType)
        {
            if (string.IsNullOrEmpty(contentType)) return false;

            return contentType.Contains("application/json") ||
                   contentType.Contains("text") ||
                   contentType.Contains("application/xml") ||
                   contentType.Contains("application/javascript");
        }

        private static string Truncate(string value, int maxLength)
        {
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...[truncated]";
        }
    }
}

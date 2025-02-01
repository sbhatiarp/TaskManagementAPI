using System.Net;
using System.Text.Json;

namespace TaskManagement.API.Middleware
{
    /// <summary>
    /// Middleware to handle exceptions and provide a structured error response.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/>.
        /// </summary>
        /// <param name="next">Next middleware in the pipeline.</param>
        /// <param name="logger">Logger instance for logging errors.</param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Middleware execution method to catch and handle exceptions.
        /// </summary>
        /// <param name="context">HTTP context.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                await HandleExceptionAsync(context, error);
            }
        }

        /// <summary>
        /// Handles exceptions and sends a formatted JSON response.
        /// </summary>
        /// <param name="context">HTTP context.</param>
        /// <param name="error">Exception thrown.</param>
        private async Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            _logger.LogError(error, "An error occurred: {Message}", error.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            var response = new
            {
                error = new
                {
                    message = error.Message,
                    statusCode = context.Response.StatusCode
                }
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}

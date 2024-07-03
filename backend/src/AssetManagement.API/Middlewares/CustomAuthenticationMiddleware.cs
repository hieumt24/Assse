using AssetManagement.Domain.Exceptions;

namespace AssetManagement.API.Middlewares
{
    public class CustomAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomAuthenticationMiddleware> _logger;

        public CustomAuthenticationMiddleware(RequestDelegate next, ILogger<CustomAuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException)
            {
                await HandleExceptionAsync(context, StatusCodes.Status401Unauthorized, "Unauthorized: Access is denied.");
            }
            catch (ForbidException)
            {
                await HandleExceptionAsync(context, StatusCodes.Status403Forbidden, "Forbidden: You do not have permission to access this resource.");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }

        private Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(new { error = message }.ToString());
        }
    }
}
using Exceptionless;
using Newtonsoft.Json;
using Vaelastrasz.Library.Exceptions;

namespace Vaelastrasz.Server.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //Serilog
                _logger.LogError(ex, ex.Message);

                // Exceptionless
                ex.ToExceptionless().Submit();

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                BadGatewayException => StatusCodes.Status502BadGateway,
                BadRequestException => StatusCodes.Status400BadRequest,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                ForbiddenException => StatusCodes.Status401Unauthorized,
                NotFoundException => StatusCodes.Status404NotFound,
                ConflictException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                statusCode,
                message = exception.Message,
                exceptionType = exception.GetType().Name,
                detail = exception.StackTrace // You can include more details for development purposes
            };

            // Serialize the response object to JSON
            var jsonResponse = JsonConvert.SerializeObject(response);

            // Write the response to the output
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
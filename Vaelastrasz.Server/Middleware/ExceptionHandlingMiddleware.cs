using Newtonsoft.Json;
using System.Net;
using Vaelastrasz.Library.Exceptions;

namespace Vaelastrasz.Server.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is ArgumentException)
            {
                code = HttpStatusCode.BadRequest; // 400
            }
            else if (exception is UnauthorizedAccessException)
            {
                code = HttpStatusCode.Unauthorized; // 401
            }
            else if (exception is ResultException)
            {
                code = HttpStatusCode.Forbidden; // 403
            }

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}

using Exceptionless;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using Vaelastrasz.Server.Controllers;

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
                //Serilog
                Log.Write(Serilog.Events.LogEventLevel.Error, ex, ex.Message);

                // Exceptionless
                ex.ToExceptionless().Submit();
                await HandleExceptionAsync(context, ex.InnerException);
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
            //else if (exception is PermissionDeniedException)
            //{
            //    code = HttpStatusCode.Forbidden; // 403
            //}
            //else if (exception is ResourceNotFoundException)
            //{
            //    code = HttpStatusCode.NotFound; // 404
            //}
            //else if (exception is ConflictException)
            //{
            //    code = HttpStatusCode.Conflict; // 409
            //}

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}

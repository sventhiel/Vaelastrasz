using Exceptionless;
using Newtonsoft.Json;
using System.Net;
using Vaelastrasz.Library.Exceptions;

namespace Vaelastrasz.Server.Middleware
{
    /// <summary>
    ///
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        /// <summary>
        ///
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is BadRequestException)
            {
                code = HttpStatusCode.BadRequest; // 400
            }
            else if (exception is UnauthorizedException) // 401
            {
                code = HttpStatusCode.Unauthorized; //401
            }
            else if (exception is ForbidException)
            {
                code = HttpStatusCode.Forbidden; // 403
            }
            else if (exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound; // 404
            }
            else if (exception is ConflictException)
            {
                code = HttpStatusCode.Conflict; // 409
            }

            var result = JsonConvert.SerializeObject(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
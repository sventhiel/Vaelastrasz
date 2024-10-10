using Exceptionless;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using RestSharp;
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
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //Serilog
                _logger.LogError(ex, ex.Message);

                // Exceptionless
                ex.ToExceptionless().Submit();

                var restResponse = await HandleExceptionAsync(ex);

                // Write response to HttpContext (convert RestResponse to HttpResponse)
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)restResponse.StatusCode;
                await httpContext.Response.WriteAsJsonAsync(restResponse.Content);
            }
        }

        public async Task<RestResponse> HandleExceptionAsync(Exception exception)
        {
            var restResponse = new RestResponse
            {
                Content = $"An error occurred: {exception.Message}",
                ErrorException = exception,
                StatusCode = exception switch
                {
                    ArgumentException => HttpStatusCode.BadRequest,
                    BadRequestException => HttpStatusCode.BadRequest,
                    UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                    UnauthorizedException => HttpStatusCode.Unauthorized,
                    ForbidException => HttpStatusCode.Forbidden,
                    KeyNotFoundException => HttpStatusCode.NotFound,
                    NotFoundException => HttpStatusCode.NotFound,
                    ConflictException => HttpStatusCode.Conflict,
                    _ => HttpStatusCode.InternalServerError
                },
                StatusDescription = "Error",
            };

            return await Task.FromResult(restResponse);
        }
    }
}
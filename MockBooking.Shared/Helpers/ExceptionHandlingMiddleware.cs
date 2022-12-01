using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Amazon.Runtime.Internal;

namespace MockBooking.Shared.Helpers
{

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next)//, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
           // _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (exception)
            {
                case UnauthorizedAccessException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case ApplicationException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = exception?.Message });
            await response.WriteAsync(result);
      
        }
    }
}

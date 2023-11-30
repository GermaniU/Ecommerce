using System.Net;
using Ecommerce.Api.Errors;
using Ecommerce.Application.Exceptions;
using Newtonsoft.Json;

namespace Ecommerce.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext, IWebHostEnvironment env)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                httpContext.Response.ContentType = "application/json";
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var result = string.Empty;

                switch (ex)
                {

                    case NotFoundException notFoundException:
                        httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case FluentValidation.ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        var errors = validationException.Errors.Select(x => x.ErrorMessage).ToArray();
                        var validationJson = JsonConvert.SerializeObject(errors);
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, errors, validationJson));
                        break;
                    case BadRequestException badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                if (string.IsNullOrWhiteSpace(result))
                {
                    result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, new string[] { ex.Message }, ex.StackTrace));
                }

                httpContext.Response.StatusCode = statusCode;

                await httpContext.Response.WriteAsync(result);
            }
        }

    }
}

using RankUpp.Core.DTOs;
using RankUpp.Core.Exceptions;
using System.Text.Json;

namespace RankUpp.Api.Helpers
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;

            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                _logger.LogInformation("Incoming request: {Method} {Path}", context.Request.Method, context.Request.Path);

                await _next(context);
            }
            catch (WrongEmailOrPasswordException)
            {
                await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, ErrorMessages.InvalidEmailOrPassword);
            }
            catch (EmailAlreadyInUseException)
            {
                await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, nameof(EmailAlreadyInUseException));
            }
            catch (ImageIsToLargeException)
            {
                await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, nameof(ImageIsToLargeException));
            }
            catch (InvalidIdException)
            {
                await HandleExceptionAsync(context, StatusCodes.Status404NotFound, ErrorMessages.InvalidId);
            }
            catch(AppSettingNotFoundException)
            {
                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, ErrorMessages.AppSettingsNotFound);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string errorMessage)
        {
            context.Response.StatusCode = statusCode;

            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponseDTO(errorMessage)));
        }
    }
}

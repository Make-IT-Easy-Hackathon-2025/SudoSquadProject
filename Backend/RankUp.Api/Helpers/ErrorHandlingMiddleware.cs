using RankUpp.Core.DTOs;
using RankUpp.Core.Exceptions;
using System.Text.Json;

namespace RankUpp.Api.Helpers
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
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

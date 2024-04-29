using Core.Common.Exceptions;
using System.Text.Json;
using Web.Common.Dtos.CommonDtos;

namespace Web.Common.Middlewares;

public class InterceptorExceptionsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<InterceptorExceptionsMiddleware> _logger;

    public InterceptorExceptionsMiddleware(
        RequestDelegate next,
        ILogger<InterceptorExceptionsMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            _logger.LogInformation($"Запрос: {context.Request.Path}");
            await _next(context);
        }
        catch (AppException ex)
        {
            string message = $"\nStatusCode - {ex.StatusCode}\nEndpoint - {context.Request.Path}\nMessage - {ex.Message}";

            if (ex.StatusCode == 500) _logger.LogError(message);
            else _logger.LogWarning(message);

            await HandleEcxeptionAsync(context, ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            await HandleEcxeptionAsync(context, 500, ex.Message);
        }
    }

    private async Task HandleEcxeptionAsync(
        HttpContext context,
        int statusCode,
        string message)
    {
        HttpResponse response = context.Response;

        response.ContentType = "application/json";
        response.StatusCode = statusCode;

        var error = new ResponseDto
        {
            Description = message,
            StatusCode = statusCode,
        };

        string result = JsonSerializer.Serialize(error);

        await response.WriteAsync(result);
    }
}

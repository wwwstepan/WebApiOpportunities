using System.Net;
using System.Text.Json;
using WebApiOpportunities.Contracts;

namespace WebApiOpportunities.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate? _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate? next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (KeyNotFoundException ex)
        {
            await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.NotFound, "Key not found");
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.InternalServerError, "Internal server error");
        }
    }

    public async Task HandleExceptionAsync(
        HttpContext httpContext,
        string exceptionMessage,
        HttpStatusCode httpStatusCode,
        string message
        )
    {
        _logger.LogError(exceptionMessage);

        var response = httpContext.Response;
        response.ContentType = "application/json";
        response.StatusCode = (int)httpStatusCode;

        var errorDto = new ErrorDto
        {
            Code = response.StatusCode,
            Message = message
        };

        var result = JsonSerializer.Serialize(errorDto);

        await response.WriteAsync(result);
    }
}

using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ToDoList.Application.Exceptions;

namespace ToDoList.Api.Middlewares;

public class GlobalErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
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

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        string message;
        HttpStatusCode statusCode;

        var exceptionType = ex.GetType();

        if (exceptionType == typeof(BadRequestException))
        {
            message = $"BadRequest error: {ex.Message}";
            statusCode = HttpStatusCode.BadRequest;
        }
        else if (exceptionType == typeof(NotImplementedException))
        {
            message = $"Not implemented error: {ex.Message}";
            statusCode = HttpStatusCode.NotImplemented;
        }
        else if (exceptionType == typeof(NotFoundException))
        {
            message = $"Not found error: {ex.Message}";
            statusCode = HttpStatusCode.NotFound;
        }
        else if(exceptionType == typeof(UnauthorizedAccessException))
        {
            message = $"Unauthorized error: {ex.Message}";
            statusCode = HttpStatusCode.Unauthorized;
        }
        else
        {
            message = $"Internal server error: {ex.Message}";
            statusCode = HttpStatusCode.InternalServerError;
        }

        var exceptionResult = JsonSerializer.Serialize(new { error = message, status = statusCode });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(exceptionResult);
    }
}
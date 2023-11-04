using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ToDoList.Security.Exceptions;

namespace ToDoList.Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
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
        HttpStatusCode statusCode;
        string message;

        var exceptionType = ex.GetType();

        if (exceptionType == typeof(BadRequestException))
        {
            message = ex.Message;
            statusCode = HttpStatusCode.BadRequest;
        }
        else if (exceptionType == typeof(ArgumentNullException))
        {
            message = ex.Message;
            statusCode = HttpStatusCode.BadRequest;
        }
        else if (exceptionType == typeof(UnauthorizedException))
        {
            message = ex.Message;
            statusCode = HttpStatusCode.Unauthorized;
        }
        else if (exceptionType == typeof(NotFoundException))
        {
            message = ex.Message;
            statusCode = HttpStatusCode.NotFound;
        }
        else if (exceptionType == typeof(ConflictException))
        {
            message = ex.Message;
            statusCode = HttpStatusCode.Conflict;
        }
        else
        {
            message = ex.Message;
            statusCode = HttpStatusCode.InternalServerError;
        }

        var exceptionResult = JsonSerializer.Serialize(new { error = message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(exceptionResult);
    }
}
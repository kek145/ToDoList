using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ToDoList.Identity.Domain.Responses;
using ToDoList.Identity.Application.Exceptions;
using ToDoList.Identity.Application.Helpers;

namespace ToDoList.Identity.Api.Middlewares;

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
            statusCode = HttpStatusCode.BadRequest;
            message = $"BadRequest exception";
        }
        else if (exceptionType == typeof(NotImplementedException))
        {
            statusCode = HttpStatusCode.NotImplemented;
            message = $"NotImplemented exception";
        }
        else if (exceptionType == typeof(UnauthorizedAccessException))
        {
            statusCode = HttpStatusCode.Unauthorized;
            message = $"Unauthorized exception";
        }
        else
        {
            statusCode = HttpStatusCode.InternalServerError;
            message = $"InternalServerError exception";
        }

        var baseResponse = new BaseResponse<object>
        {
            Status = ResponseHelper.Error,
            StatusCode = statusCode,
            Message = message,
            Errors = [ex.Message]
        };

        var exceptionResult = JsonSerializer.Serialize(baseResponse);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(exceptionResult);
    }
}
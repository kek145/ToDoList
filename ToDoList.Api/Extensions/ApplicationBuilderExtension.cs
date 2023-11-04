using Microsoft.AspNetCore.Builder;
using ToDoList.Api.Middlewares;

namespace ToDoList.Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder AddGlobalErrorHandling(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<ErrorHandlerMiddleware>();
}
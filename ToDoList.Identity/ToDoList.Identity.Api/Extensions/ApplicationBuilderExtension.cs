using Microsoft.AspNetCore.Builder;
using ToDoList.Identity.Api.Middlewares;

namespace ToDoList.Identity.Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<ErrorHandlerMiddleware>();
}
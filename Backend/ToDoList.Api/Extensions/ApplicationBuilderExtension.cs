namespace ToDoList.Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<ErrorHandlerMiddleware>();
}
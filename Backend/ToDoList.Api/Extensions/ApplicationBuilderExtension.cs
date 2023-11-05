namespace ToDoList.Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static void AddGlobalErrorHandling(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<ErrorHandlerMiddleware>();
}
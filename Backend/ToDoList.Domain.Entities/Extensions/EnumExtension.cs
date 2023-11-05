namespace ToDoList.Domain.Entities.Extensions;

public static class EnumExtension
{
    public static string GetDisplayName(this System.Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName() ?? "Undefined";
    }

    public static string GetDisplayName(this bool boolValue)
    {
        return boolValue.GetType()
            .GetMember(boolValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName() ?? "Undefined";
    }
}
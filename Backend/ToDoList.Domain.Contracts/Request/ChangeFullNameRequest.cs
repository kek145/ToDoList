namespace ToDoList.Domain.Contracts.Request;

public class ChangeFullNameRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
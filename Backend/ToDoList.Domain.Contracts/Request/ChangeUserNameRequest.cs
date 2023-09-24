namespace ToDoList.Domain.Contracts.Request;

public class ChangeUserNameRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
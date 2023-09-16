namespace ToDoList.Domain.Contracts.Response;

public class GetUserResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
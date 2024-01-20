namespace ToDoList.Identity.Domain.Responses;

public class UserResponse
{
    public long UserId { get; set; }
    public string Email { get; set; } = string.Empty;
}
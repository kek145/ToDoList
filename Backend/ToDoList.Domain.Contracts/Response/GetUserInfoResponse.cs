namespace ToDoList.Domain.Contracts.Response;

public class GetUserInfoResponse
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
}
namespace ToDoList.Services.Interfaces
{
    public interface IJwtTokenService
    {
        int? GetUserIdFromToken(string token);
    }
}
using System.Threading.Tasks;

namespace ToDoList.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(string email, string password);
        Task<string> GenerateTokenAsync(string username);
        Task<bool> ValidateTokenAsync(string token);
    }
}

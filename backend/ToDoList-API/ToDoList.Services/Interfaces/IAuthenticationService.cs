using System.Threading.Tasks;
using ToDoList.Domain.Entity;

namespace ToDoList.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateAsync(string email, string password);
        string GenerateToken(UserEntity entity);
    }
}

using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using ToDoList.Models.Dto;

namespace ToDoList.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateAsync(string email, string password);
        string GenerateToken(UserEntity entity);
    }
}

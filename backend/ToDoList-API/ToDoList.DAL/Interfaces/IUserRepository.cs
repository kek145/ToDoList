using System.Threading.Tasks;
using ToDoList.Domain.Entity;

namespace ToDoList.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task DeleteUserAsync(int id);
        Task CreateUserAsync(UserEntity user);
        Task UpdateUserAsync(UserEntity user);
        Task<UserEntity> FindByIdAsync(int userid);
        Task<UserEntity> FindByEmailAsync(string email);
        Task<UserEntity> FindByUserNameAsync(string username);
    }
}

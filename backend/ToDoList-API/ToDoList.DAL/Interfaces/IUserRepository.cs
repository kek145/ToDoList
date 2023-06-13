using System.Threading.Tasks;
using ToDoList.Configurations.Configurations;
using ToDoList.Domain.Entity;

namespace ToDoList.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(UserEntity user);
        Task UpdateUserAsync(UserEntity user);
        Task<UserEntity> FindByIdAsync(int userid);
        Task<UserEntity> FindByEmailAsync(string email);
    }
}

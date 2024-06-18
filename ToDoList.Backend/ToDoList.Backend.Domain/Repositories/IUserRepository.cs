using System.Linq;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;

namespace ToDoList.Domain.Repositories;

public interface IUserRepository
{
    Task<int> DeleteUserAsync(int userId, CancellationToken cancellationToken = default);
    Task<UserDto?> GetUserById(int userId, CancellationToken cancellationToken = default);
    Task<string> GetUserFullName(int userId, CancellationToken cancellationToken = default);
    Task<UserDto?> GetUserByEmail(string email, CancellationToken cancellationToken = default);
    Task<UserDto> AddUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
}
using System.Linq;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;

namespace ToDoList.Domain.Repositories;

public interface IUserRepository
{
    IQueryable<UserDto> GetAll();
    Task<UserDto> AddUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
}
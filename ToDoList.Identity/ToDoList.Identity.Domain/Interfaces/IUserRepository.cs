using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Identity.Domain.Dto;

namespace ToDoList.Identity.Domain.Interfaces;

public interface IUserRepository
{
    IQueryable<UserDto> GetAll();
    Task<UserDto> AddUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
}
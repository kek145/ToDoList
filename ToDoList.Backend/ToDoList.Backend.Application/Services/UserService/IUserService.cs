using System.Threading.Tasks;
using ToDoList.Domain.Response;

namespace ToDoList.Application.Services.UserService;

public interface IUserService
{
    Task<UserFullNameResponse> GetUserFullName();
}
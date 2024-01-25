using System.Threading.Tasks;
using ToDoList.Domain.Request;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Services.RegistrationService;

public interface IRegistrationService
{
    Task<UserResponse> RegistrationAsync(RegistrationRequest request);
}
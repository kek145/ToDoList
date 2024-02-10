using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Request;

namespace ToDoList.Application.Services.AuthenticationService;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> LoginAsync(LoginRequest request);
}
using System.Threading.Tasks;
using ToDoList.Domain.Request;
using ToDoList.Domain.Response;

namespace ToDoList.Application.Services.AuthenticationService;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> LoginAsync(LoginRequest request);
}
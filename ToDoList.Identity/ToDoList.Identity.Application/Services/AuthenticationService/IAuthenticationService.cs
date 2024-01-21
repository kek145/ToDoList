using System.Threading.Tasks;
using ToDoList.Identity.Domain.Requests;
using ToDoList.Identity.Domain.Responses;

namespace ToDoList.Identity.Application.Services.AuthenticationService;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> LoginAsync(LoginRequest request);
}
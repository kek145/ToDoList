using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Request;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Services.AuthenticationService;

public interface IAuthenticationService
{
    Task<IBaseResponse<AuthenticationResponse>> LoginAsync(LoginRequest request);
}
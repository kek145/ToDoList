using ToDoList.Identity.Domain.Responses;

namespace ToDoList.Identity.Application.Services.TokenService;

public interface ITokenService
{
    AuthenticationResponse GenerateTokens(UserResponse response);
}
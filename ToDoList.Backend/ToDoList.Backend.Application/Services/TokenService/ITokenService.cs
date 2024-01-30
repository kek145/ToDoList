using System.Threading.Tasks;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Services.TokenService;

public interface ITokenService
{
    Task DeleteTokenAsync(int userId);
    Task<AuthenticationResponse> RefreshTokenAsync();
    Task SaveTokenAsync(int userId, string refreshToken);
    AuthenticationResponse GenerateTokens(UserResponse response);
}
using ToDoList.Domain.Entities.DbSet;

namespace ToDoList.BL.Services.TokenService;

public interface ITokenService
{
    Task SaveTokenAsync(int userId, string refreshToken);
    Task<bool> ValidationRefreshTokenAsync(string refreshToken);
    AuthenticationResponse GenerateTokens(GetUserResponse entity);
    Task<AuthenticationResponse> RefreshTokenAsync(string refreshToken);
}
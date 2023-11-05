namespace ToDoList.BL.Services.AuthenticationService;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest request);
}
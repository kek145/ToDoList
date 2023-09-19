namespace ToDoList.BL.Services.RegistrationService;

public interface IRegistrationService
{
    Task<GetUserResponse> RegistrationAsync(RegistrationRequest request);
}
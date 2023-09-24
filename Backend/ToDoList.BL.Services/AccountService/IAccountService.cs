namespace ToDoList.BL.Services.AccountService;

public interface IAccountService
{
    Task DeleteAccountAsync(int userId);
    Task<GetUserInfoResponse> GetUserInfoAsync(int userId);
    Task<GetUserFullNameResponse> GetUserFullNameAsync(int userId);
    Task UpdateEmailAsync(ChangeEmailRequest request, int userId);
    Task UpdateFullNameAsync(ChangeFullNameRequest request, int userId);
}
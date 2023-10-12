namespace ToDoList.BL.Services.AccountService;

public interface IAccountService
{
    Task DeleteAccountAsync();
    Task<GetUserInfoResponse> GetUserInfoAsync();
    Task UpdateEmailAsync(ChangeEmailRequest request);
    Task<GetUserFullNameResponse> GetUserFullNameAsync();
    Task UpdatePasswordAsync(ChangePasswordRequest request);
    Task UpdateFullNameAsync(ChangeFullNameRequest request);
}
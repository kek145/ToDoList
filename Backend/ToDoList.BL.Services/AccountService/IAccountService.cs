namespace ToDoList.BL.Services.AccountService;

public interface IAccountService
{
    Task<GetUserInfoResponse> GetUserInfoAsync(int userId);
    Task<GetUserFullNameResponse> GetUserFullNameAsync(int userId);
}
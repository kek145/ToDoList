namespace ToDoList.BL.Services.AccountService;

public interface IAccountService
{
    Task DeleteAccountAsync();
    Task<GetUserFullNameResponse> GetUserFullNameAsync();
}
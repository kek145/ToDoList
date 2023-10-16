using Microsoft.AspNetCore.Http;

namespace ToDoList.BL.Services.AccountService;

public class AccountService : IAccountService
{
    private string _userId = string.Empty;
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountService(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task DeleteAccountAsync()
    {
        if (_httpContextAccessor.HttpContext is not null)
            _userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId")!.Value;
        else
            throw new UnauthorizedException("User is not found");
        
        var result = await _mediator.Send(new DeleteUserCommand(Convert.ToInt32(_userId)));

        if (!result)
            throw new UnauthorizedException("User not found");
    }
    

    public async Task<GetUserFullNameResponse> GetUserFullNameAsync()
    {
        if (_httpContextAccessor.HttpContext is not null)
            _userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId")!.Value;
        else
            throw new UnauthorizedException("User is not found");
        
        var result = await _mediator.Send(new GetUserFullNameQuery(Convert.ToInt32(_userId)));

        if (result == null)
            throw new NotFoundException("User not found");

        return result;
    }
}
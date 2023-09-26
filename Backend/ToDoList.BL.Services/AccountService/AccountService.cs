namespace ToDoList.BL.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly IMediator _mediator;

    public AccountService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DeleteAccountAsync(int userId)
    {
        var result = await _mediator.Send(new DeleteUserCommand(userId));

        if (!result)
            throw new NotFoundException("User not found");
    }

    public async Task<GetUserInfoResponse> GetUserInfoAsync(int userId)
    {
        var result = await _mediator.Send(new GetUserInfoQuery(userId));

        if (result == null)
            throw new NotFoundException("User not found");

        return result;
    }

    public async Task<GetUserFullNameResponse> GetUserFullNameAsync(int userId)
    {
        var result = await _mediator.Send(new GetUserFullNameQuery(userId));

        if (result == null)
            throw new NotFoundException("User not found");

        return result;
    }

    public async Task UpdatePasswordAsync(ChangePasswordRequest request, int userId)
    {
        var result = await _mediator.Send(new UpdatePasswordCommand(userId, request));
        
        if(!result)
            throw new BadRequestException("The old password is incorrect. Please make sure you have entered your current password correctly and try again.");
    }

    public async Task UpdateEmailAsync(ChangeEmailRequest request, int userId)
    {
        var result = await _mediator.Send(new UpdateEmailCommand(userId, request));
        
        if (!result)
            throw new NotFoundException("User not found");
    }

    public async Task UpdateFullNameAsync(ChangeFullNameRequest request, int userId)
    {
        var result = await _mediator.Send(new UpdateFullNameCommand(userId, request));

        if (!result)
            throw new NotFoundException("User not found");
    }
}
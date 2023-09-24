﻿namespace ToDoList.BL.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly IMediator _mediator;

    public AccountService(IMediator mediator)
    {
        _mediator = mediator;
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
}
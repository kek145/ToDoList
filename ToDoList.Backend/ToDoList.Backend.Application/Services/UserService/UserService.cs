using System;
using MediatR;
using System.Net;
using System.Threading.Tasks;
using ToDoList.Domain.Response;
using Microsoft.AspNetCore.Http;
using ToDoList.Application.Exceptions;
using ToDoList.Application.Commands.Users.Delete;
using ToDoList.Application.Queries.Users.GetFullName;

namespace ToDoList.Application.Services.UserService;

public class UserService(IMediator mediator, IHttpContextAccessor httpContextAccessor) : IUserService
{
    private readonly IMediator _mediator = mediator;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<HttpStatusCode> DeleteUserAccountAsync()
    {
        var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        if (userId <= 0)
            throw new BadRequestException("Id cannot be less than or equal to zero!");

        var command = new DeleteUserCommand(userId);

        var result = await _mediator.Send(command);

        return result > 0 ? HttpStatusCode.NoContent : HttpStatusCode.NotFound;
    }

    public async Task<UserFullNameResponse> GetUserFullNameAsync()
    {
        var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
        
        var query = new GetUserFullNameQuery(userId);
        
        var result = await _mediator.Send(query);

        return result;
    }
}
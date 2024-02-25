using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using ToDoList.Application.Queries.Users.GetFullName;
using ToDoList.Domain.Response;

namespace ToDoList.Application.Services.UserService;

public class UserService(IMediator mediator, IHttpContextAccessor httpContextAccessor) : IUserService
{
    private readonly IMediator _mediator = mediator;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    
    public async Task<UserFullNameResponse> GetUserFullName()
    {
        var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value);

        Console.WriteLine($"----- UserId: {userId} --------");
        
        var query = new GetUserFullNameQuery(userId);
        
        var result = await _mediator.Send(query);

        return result;
    }
}
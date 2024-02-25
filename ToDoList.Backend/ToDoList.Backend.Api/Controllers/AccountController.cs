using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Services.UserService;
using ToDoList.Domain.Response;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/accounts")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AccountController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;
    
    [HttpGet]
    [Route("full-name")]
    public async Task<ActionResult<UserFullNameResponse>> GetUserFull()
    {
        var response = await _userService.GetUserFullName();
        return Ok(response);
    }
}
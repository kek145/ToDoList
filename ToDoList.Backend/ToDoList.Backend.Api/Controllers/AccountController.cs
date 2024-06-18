using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Response;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Application.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
        var response = await _userService.GetUserFullNameAsync();
        return Ok(response);
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteAccountAsync()
    {
        var result = await _userService.DeleteUserAccountAsync();
        return StatusCode((int)result);
    }
}
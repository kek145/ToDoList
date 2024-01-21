using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Identity.Domain.Requests;
using ToDoList.Identity.Application.Services.AuthenticationService;

namespace ToDoList.Identity.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class LoginController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public LoginController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
    {
        var response = await _authenticationService.LoginAsync(request);
        return Ok(response);
    }
}
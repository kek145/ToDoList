using System.Threading.Tasks;
using ToDoList.Domain.Request;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Services.RegistrationService;
using ToDoList.Application.Services.AuthenticationService;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    private readonly IRegistrationService _registrationService;
    private readonly IAuthenticationService _authenticationService;

    public IdentityController(IRegistrationService registrationService, IAuthenticationService authenticationService)
    {
        _registrationService = registrationService;
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("registration")]
    public async Task<IActionResult> Registration([FromBody] RegistrationRequest request)
    {
        return Ok();
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return Ok();
    }
}
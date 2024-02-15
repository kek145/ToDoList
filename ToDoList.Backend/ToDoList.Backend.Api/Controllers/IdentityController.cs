using System;
using System.Threading.Tasks;
using ToDoList.Domain.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ToDoList.Application.Services.RegistrationService;
using ToDoList.Application.Services.AuthenticationService;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController(IRegistrationService registrationService, IAuthenticationService authenticationService) : ControllerBase
{
    private readonly IRegistrationService _registrationService = registrationService;
    private readonly IAuthenticationService _authenticationService = authenticationService;
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await _authenticationService.LoginAsync(request);
        return Ok(response);
    }

    [HttpPost]
    [Route("registration")]
    public async Task<IActionResult> Registration([FromBody] RegistrationRequest request)
    {
        var response = await _registrationService.RegistrationAsync(request);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [Route("auth")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult GetUserId()
    {
        var userId = Convert.ToInt32(HttpContext.User.FindFirst("UserId")?.Value);

        if (userId <= 0)
            return Unauthorized(new { error = "error 401" });
        
        return Ok(new { Id = userId });
    }
}
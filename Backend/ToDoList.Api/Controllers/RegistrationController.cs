using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.BL.Services.RegistrationService;
using ToDoList.Domain.Contracts.Request;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    [HttpPost]
    [Route("Sign-Up")]
    public async Task<IActionResult> Registration([FromBody] RegistrationRequest request)
    {
        var result = await _registrationService.RegistrationAsync(request);

        if (result == null!)
            return Conflict(new { error = "User is registered" });

        return StatusCode(201, result);
    }
}
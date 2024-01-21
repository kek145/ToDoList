using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Identity.Domain.Requests;
using ToDoList.Identity.Application.Services.RegistrationService;

namespace ToDoList.Identity.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    [HttpPost]
    [Route("registration")]
    public async Task<IActionResult> RegistrationUser([FromBody] RegistrationRequest request)
    {
        var response = await _registrationService.RegistrationUserAsync(request);
        return StatusCode((int)response.StatusCode,response);
    }
}
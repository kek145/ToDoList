namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    [HttpPost]
    public async Task<IActionResult> RegistrationAccount([FromBody] RegistrationRequest request)
    {
        var result = await _registrationService.RegistrationAsync(request);
        return StatusCode(201, result);
    }
}
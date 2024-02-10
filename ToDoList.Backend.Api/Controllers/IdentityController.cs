using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Backend.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }
    
    [HttpPost]
    [Route("registration")]
    public async Task<IActionResult> Registration()
    {
        return Ok();
    }
}
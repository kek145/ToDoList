using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Backend.Api.Controllers;

[ApiController]
[Route("api/refresh-token")]
public class RefreshTokenController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RefreshToken()
    {
        return Ok();
    }
}
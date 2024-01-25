using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api")]
public class RefreshTokenController : ControllerBase
{
    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        return Ok();
    }
}
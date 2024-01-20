using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Identity.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class RefreshTokenController : ControllerBase
{

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        return Ok();
    }
}
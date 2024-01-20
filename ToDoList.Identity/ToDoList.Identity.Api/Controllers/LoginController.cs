using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Identity.Domain.Requests;

namespace ToDoList.Identity.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class LoginController : ControllerBase
{

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
    {
        return Ok();
    }
}
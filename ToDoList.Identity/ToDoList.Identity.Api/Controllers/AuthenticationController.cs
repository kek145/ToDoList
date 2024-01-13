using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Identity.Api.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    [HttpPost]
    [Route("sign-in")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> Authentication()
    {
        return Ok();
    }
}
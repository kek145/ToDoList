using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Identity.Api.Controllers;

[ApiController]
[Route("api/registration")]
public class RegistrationController : ControllerBase
{

    [HttpPost]
    [Route("sign-up")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Registration()
    {
        return CreatedAtAction(null, new { id = 1 }, null);
    }
}
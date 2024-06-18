using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Services.TokenService;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api")]
public class RefreshTokenController(ITokenService tokenService) : ControllerBase
{
    private readonly ITokenService _tokenService = tokenService;

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var response = await _tokenService.RefreshTokenAsync();
        return Ok(response);
    }
}
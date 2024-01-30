using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Services.TokenService;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api")]
public class RefreshTokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public RefreshTokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var response = await _tokenService.RefreshTokenAsync();
        return Ok(response);
    }
}
namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/token")]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        
        if (refreshToken == null)
            return Unauthorized("Token not found!");
        
        await _tokenService.ValidationRefreshTokenAsync(refreshToken);
        
        var response = await _tokenService.RefreshTokenAsync(refreshToken);

        if (response == null!)
            return Unauthorized(new { error = "The provided refresh token is invalid or expired." });
        
        Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
        });
        
        return Ok(response);
    }
}
namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AccountController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IAccountService _accountService;

    public AccountController(ITokenService tokenService, IAccountService accountService)
    {
        _tokenService = tokenService;
        _accountService = accountService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserInfo()
    {
        var result = await _accountService.GetUserInfoAsync();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserFullName()
    {
        var result = await _accountService.GetUserFullNameAsync();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEmail([FromBody] ChangeEmailRequest request)
    {
        await _accountService.UpdateEmailAsync(request);
        
        var refreshToken = Request.Cookies["refreshToken"];

        if (refreshToken == null)
            return NotFound(new { error = "RefreshToken not found" });
        
        await _tokenService.DeleteTokenAsync(refreshToken);
        
        return Unauthorized(new { description = "The operation was successful but re-authentication is required" });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFullName([FromBody] ChangeFullNameRequest request)
    {
        await _accountService.UpdateFullNameAsync(request);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePassword([FromBody] ChangePasswordRequest request)
    {
        await _accountService.UpdatePasswordAsync(request);
        
        var refreshToken = Request.Cookies["refreshToken"];

        if (refreshToken == null)
            return NotFound(new { error = "RefreshToken not found" });
        
        await _tokenService.DeleteTokenAsync(refreshToken);
        
        return Unauthorized(new { description = "The operation was successful but re-authentication is required" });
        
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAccount()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (refreshToken == null)
            return NotFound(new { error = "RefreshToken not found" });
        
        await _tokenService.DeleteTokenAsync(refreshToken);
        await _accountService.DeleteAccountAsync();

        return NoContent();
    }
    
    [HttpDelete]
    public async Task<IActionResult> LogoutAccount()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (refreshToken == null)
            return NotFound(new { error = "RefreshToken not found" });

        await _tokenService.DeleteTokenAsync(refreshToken);
        
        Response.Cookies.Delete(refreshToken);

        return NoContent();
    }
}
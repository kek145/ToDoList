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
        var userId = User.FindFirst("UserId")?.Value;
        
        if (userId is null or "0")
            return Unauthorized(new { error = "User not found!" });

        var result = await _accountService.GetUserInfoAsync(Convert.ToInt32(userId));

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserFullName()
    {
        var userId = User.FindFirst("UserId")?.Value;
        
        if (userId is null or "0")
            return Unauthorized(new { error = "User not found!" });

        var result = await _accountService.GetUserFullNameAsync(Convert.ToInt32(userId));
        
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAccount()
    {
        var userId = User.FindFirst("UserId")?.Value;
        
        if (userId is null or "0")
            return Unauthorized(new { error = "User not found!" });

        await _accountService.DeleteAccountAsync(Convert.ToInt32(userId));

        return NoContent();
    }
    
    [HttpDelete]
    public async Task<IActionResult> LogoutAccount()
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (userId is null or "0")
            return Unauthorized(new { error = "User not found!" });
        
        var refreshToken = Request.Cookies["refreshToken"];

        if (refreshToken == null)
            return NotFound(new { error = "RefreshToken not found" });


        await _tokenService.DeleteTokenAsync(refreshToken);
        
        Response.Cookies.Delete(refreshToken);

        return NoContent();
    }
}
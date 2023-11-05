namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/account")]
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
    [Route("get-fullname")]
    public async Task<IActionResult> GetUserFullName()
    {
        var result = await _accountService.GetUserFullNameAsync();
        return Ok(result);
    }

    [HttpDelete]
    [Route("delete-account")]
    public async Task<IActionResult> DeleteAccount()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (refreshToken == null)
            return NotFound(new { error = "RefreshToken not found" });
        
        await _tokenService.DeleteTokenAsync(refreshToken);
        await _accountService.DeleteAccountAsync();

        return NoContent();
    }
    
    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> LogoutAccount()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (refreshToken == null)
            return Unauthorized(new { error = "Token not found" });
        
        await _tokenService.DeleteTokenAsync(refreshToken);
        
        Response.Cookies.Delete("refreshToken");

        return Unauthorized();
    }
}
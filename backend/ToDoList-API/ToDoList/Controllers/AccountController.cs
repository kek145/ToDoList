using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using ToDoList.Models.Dto;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.Services.Validators;
using ToDoList.Services.Interfaces;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IRegistrationService _registrationService;
        private readonly IAuthenticationService _authenticationService;
        public AccountController(ILogger<AccountController> logger, IRegistrationService registrationService, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _registrationService = registrationService;
            _authenticationService = authenticationService;
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAccount()
        {
            var token = HttpContext?.Request?.Headers?["Authorization"]!.FirstOrDefault()!.Split(" ").Last();

            if(token != null!)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var userId = jwtToken?.Claims?.FirstOrDefault(c => c?.Type == "Sub")?.Value;
                Response.Cookies.Delete(userId!);
            }

            return Ok(new { message = "Logout successful" });
        }

        [HttpPost("LoginAccount")]
        public async Task<IActionResult> LoginAccount([FromBody] LoginDto loginDto)
        {
            string token = await _authenticationService.AuthenticateAsync(loginDto.Email, loginDto.Password);

            if (token == null!)
                return Unauthorized("Wrong email or password!");

            return Ok(new { Token = token });
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] RegisterDto userDto)
        {
            var validator = new DataValidator();
            bool emailExists = await _registrationService.IsEmailExistsAsync(userDto.Email);
            if (emailExists)
                return BadRequest("User with the same email already exists!");
            if (!validator.ValidateFieldsNotEmpty(userDto.UserName, userDto.Email, userDto.Password))
                return BadRequest("Fields cannot be empty!");
            if (!validator.CheckWhitespace(userDto.UserName, userDto.Email, userDto.Password, userDto.ConfirmPassword))
                return BadRequest("The entered data cannot contain spaces!");
            if (!validator.ContainsRussian(userDto.UserName, userDto.Email, userDto.Password))
                return BadRequest("The entered data cannot contain Russian characters!");
            if (!validator.IsEmailValid(userDto.Email))
                return BadRequest("Mail cannot contain characters!");
            if (userDto.Password != userDto.ConfirmPassword)
                return BadRequest("Password mismatch!");


            await _registrationService.RegisterAsync(userDto.UserName, userDto.Email, userDto.Password, userDto.ConfirmPassword);

            return Ok(new { message = "Registration successful" });
        }
    }
}

using ToDoList.Dto;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.Services.Interfaces;
using ToDoList.Services.Validators;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly IRegistrationService _registrationService;
        public RegistrationController(ILogger<RegistrationController> logger, IRegistrationService registrationService)
        {
            _logger = logger;
            _registrationService = registrationService;
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] UserDto userDto)
        {
            var validator = new DataValidator();
            bool emailExists = await _registrationService.IsEmailExistsAsync(userDto.Email);

            if (emailExists)
                return Conflict("User with the same email already exists");
            if (!validator.ValidateFieldsNotEmpty(userDto.UserName, userDto.Email, userDto.Password))
                return BadRequest("Fields cannot be empty");
            if (!validator.CheckWhitespace(userDto.UserName, userDto.Email, userDto.Password))
                return BadRequest("The entered data cannot contain spaces");
            if (!validator.ContainsRussian(userDto.UserName, userDto.Email, userDto.Password))
                return BadRequest("The entered data cannot contain Russian characters");
            if (!validator.IsEmailValid(userDto.Email))
                return BadRequest("Mail cannot contain characters");


            await _registrationService.RegisterAsync(userDto.UserName, userDto.Email, userDto.Password);

            return Ok("Registration successful");
        }
    }
}

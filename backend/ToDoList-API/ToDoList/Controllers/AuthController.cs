using System;
using ToDoList.Models.Dto;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.Services.Validators;
using ToDoList.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Configurations.Configurations;
using Microsoft.AspNetCore.Http;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IRegistrationService _registrationService;
        private readonly IAuthenticationService _authenticationService;
        public AuthController(ILogger<AuthController> logger, IRegistrationService registrationService, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _registrationService = registrationService;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("LoginAccount")]
        public async Task<IActionResult> LoginAccount([FromBody] LoginDto loginDto)
        {
            string token = await _authenticationService.AuthenticateAsync(loginDto.Email, loginDto.Password);

            if (token is null)
                return Unauthorized("User is not found");

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

            return Ok("Registration successful");
        }
    }
}

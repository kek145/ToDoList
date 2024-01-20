﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Identity.Domain.Requests;

namespace ToDoList.Identity.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class RegistrationController : ControllerBase
{
    [HttpPost]
    [Route("registration")]
    public async Task<IActionResult> RegistrationUser([FromBody] RegistrationRequest request)
    {
        return Ok();
    }
}
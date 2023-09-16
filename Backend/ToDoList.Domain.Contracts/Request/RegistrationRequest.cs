﻿using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Contracts.Request;

public class RegistrationRequest
{
    [Required, MinLength(2)]
    public string FirstName { get; set; } = string.Empty;
    [Required, MinLength(2)]
    public string LastName { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;
    [Required, Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
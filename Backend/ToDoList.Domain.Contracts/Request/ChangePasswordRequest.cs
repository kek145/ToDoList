﻿namespace ToDoList.Domain.Contracts.Request;

public class ChangePasswordRequest
{
    [Required]
    public string CurrentPassword { get; set; } = string.Empty;
    [Required, MinLength(6)]
    public string NewPassword { get; set; } = string.Empty;
    [Required, Compare("NewPassword")]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}
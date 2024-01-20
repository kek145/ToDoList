using System.ComponentModel.DataAnnotations;

namespace ToDoList.Identity.Domain.Requests;

public class RegistrationRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
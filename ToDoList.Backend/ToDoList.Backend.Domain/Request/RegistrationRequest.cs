using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Request;

public class RegistrationRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
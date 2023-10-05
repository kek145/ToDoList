using System.ComponentModel.DataAnnotations;

namespace ToDoList.Services.Models.Dto
{
    public class LoginDto
    {
        [EmailAddress]
        [MaxLength(100)]
        public required string Email { get; set; }

        [MaxLength(50)] 
        public required string Password { get; set; }
    }
}

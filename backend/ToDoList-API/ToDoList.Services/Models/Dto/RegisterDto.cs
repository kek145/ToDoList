using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Dto
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

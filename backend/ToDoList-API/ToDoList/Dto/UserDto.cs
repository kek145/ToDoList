using System.ComponentModel.DataAnnotations;

namespace ToDoList.Dto
{
    public class UserDto
    {
        [MaxLength(20)]
        public required string UserName { get; set; }
        [EmailAddress]
        [MaxLength(100)]
        public required string Email { get; set; }
        [MaxLength (50)]
        public required string Password { get; set; }
        [MaxLength(50)]
        public required string ConfirmPassword { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Dto
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
    }
}

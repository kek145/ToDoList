﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Entity
{
    public class UserEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<TaskEntity>? Tasks { get; set; } = new();
    }
}
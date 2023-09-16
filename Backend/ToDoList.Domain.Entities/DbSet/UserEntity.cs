﻿using System.Collections.Generic;

namespace ToDoList.Domain.Entities.DbSet;

public class UserEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public ICollection<TaskEntity> Task { get; set; } = null!;
    public ICollection<RefreshTokenEntity> RefreshToken { get; set; } = null!;
}
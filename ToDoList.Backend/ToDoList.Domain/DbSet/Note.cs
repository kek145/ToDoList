﻿using System;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Domain.DbSet;

public class Note : IEntityId<long>, IBaseEntity
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; }
    public bool Status { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int UserId { get; set; }
}
using System;

namespace ToDoList.Domain.Abstractions;

public interface IBaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
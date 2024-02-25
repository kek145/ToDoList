﻿namespace ToDoList.Domain.Abstractions;

public interface IEntityId<T> where T : struct 
{
    T Id { get; set; }
}
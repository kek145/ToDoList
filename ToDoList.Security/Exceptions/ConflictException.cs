﻿using System;

namespace ToDoList.Security.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) {}
}
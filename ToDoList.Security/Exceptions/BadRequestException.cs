﻿using System;

namespace ToDoList.Security.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}
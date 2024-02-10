using System;

namespace ToDoList.Application.Exceptions;

[Serializable]
public class BadRequestException(string message) : Exception(message);
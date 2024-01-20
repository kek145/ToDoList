using System;

namespace ToDoList.Identity.Application.Exceptions;

[Serializable]
public class BadRequestException(string message) : Exception(message);
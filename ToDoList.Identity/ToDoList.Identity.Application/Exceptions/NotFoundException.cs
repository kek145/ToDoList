using System;

namespace ToDoList.Identity.Application.Exceptions;

[Serializable]
public class NotFoundException(string message) : Exception(message);
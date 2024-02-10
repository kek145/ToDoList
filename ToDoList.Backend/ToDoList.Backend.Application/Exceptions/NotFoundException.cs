using System;

namespace ToDoList.Application.Exceptions;

[Serializable]
public class NotFoundException(string message) : Exception(message);
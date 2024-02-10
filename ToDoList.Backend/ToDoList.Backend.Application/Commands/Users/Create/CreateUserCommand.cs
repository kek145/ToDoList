﻿using MediatR;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Request;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Commands.Users.Create;

public record CreateUserCommand(RegistrationRequest RegistrationRequest) : IRequest<UserResponse>;
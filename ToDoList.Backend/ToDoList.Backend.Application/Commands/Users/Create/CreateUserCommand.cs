using MediatR;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Request;
using ToDoList.Domain.Response;

namespace ToDoList.Application.Commands.Users.Create;

public sealed record CreateUserCommand(RegistrationRequest RegistrationRequest) : IRequest<UserResponse>;
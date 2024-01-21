using MediatR;
using ToDoList.Identity.Domain.Dto;
using ToDoList.Identity.Domain.Requests;
using ToDoList.Identity.Domain.Responses;

namespace ToDoList.Identity.Application.Commands.UserCommands;

public record CreateUserCommand(RegistrationRequest RegistrationRequest) : IRequest<UserDto>;
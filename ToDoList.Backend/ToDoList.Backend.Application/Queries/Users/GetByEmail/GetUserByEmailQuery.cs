using MediatR;
using ToDoList.Domain.Dto;

namespace ToDoList.Application.Queries.Users.GetByEmail;

public record GetUserByEmailQuery(string Email) : IRequest<UserDto>;
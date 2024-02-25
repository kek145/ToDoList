using MediatR;
using ToDoList.Domain.Response;

namespace ToDoList.Application.Queries.Users.GetFullName;

public record GetUserFullNameQuery(int UserId): IRequest<UserFullNameResponse>;
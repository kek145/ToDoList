using MediatR;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Queries.Notes.GetAll;

public sealed record GetAllNotesQuery(QueryParameters QueryParameters, int UserId) : IRequest<PagedResult<NoteDto>>;
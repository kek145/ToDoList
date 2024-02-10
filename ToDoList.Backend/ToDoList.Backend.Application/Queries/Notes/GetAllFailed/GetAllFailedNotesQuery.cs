using MediatR;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Result;
using ToDoList.Domain.Helpers;

namespace ToDoList.Application.Queries.Notes.GetAllFailed;

public sealed record GetAllFailedNotesQuery(QueryParameters QueryParameters, int UserId) : IRequest<PagedResult<NoteDto>>;
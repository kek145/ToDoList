using MediatR;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Response;

namespace ToDoList.Application.Queries.Notes.GetAllFailed;

public sealed record GetAllFailedNotesQuery(QueryParameters QueryParameters, int UserId) : IRequest<PagedResult<NoteDto>>;
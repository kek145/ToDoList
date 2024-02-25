using MediatR;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Response;

namespace ToDoList.Application.Queries.Notes.GetAllCompleted;

public record GetAllCompletedNotesQuery(QueryParameters QueryParameters, int UserId) : IRequest<PagedResult<NoteDto>>;
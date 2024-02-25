using MediatR;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Response;

namespace ToDoList.Application.Queries.Notes.GetAllByPriority;

public record GetAllByPriorityNotesQuery(QueryParameters QueryParameters, string Priority, int UserId) : IRequest<PagedResult<NoteDto>>;
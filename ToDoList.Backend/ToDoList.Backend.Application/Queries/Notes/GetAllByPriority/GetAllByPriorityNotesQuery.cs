using MediatR;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Queries.Notes.GetAllByPriority;

public record GetAllByPriorityNotesQuery(QueryParameters QueryParameters, string Priority, int UserId) : IRequest<PagedResult<NoteResponse>>;
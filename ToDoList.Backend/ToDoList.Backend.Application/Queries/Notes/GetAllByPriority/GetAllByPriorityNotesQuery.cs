using MediatR;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Result;
using ToDoList.Domain.Helpers;

namespace ToDoList.Application.Queries.Notes.GetAllByPriority;

public record GetAllByPriorityNotesQuery(QueryParameters QueryParameters, string Priority, int UserId) : IRequest<PagedResult<NoteDto>>;
using MediatR;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Queries.Notes.GetAllCompleted;

public record GetAllCompletedNotesQuery(QueryParameters QueryParameters, int UserId) : IRequest<PagedResult<NoteResponse>>;
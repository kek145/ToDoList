using MediatR;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Response;

namespace ToDoList.Application.Queries.Notes.GetById;

public sealed record GetNoteByIdQuery(long NoteId, int UserId) : IRequest<NoteDto>;
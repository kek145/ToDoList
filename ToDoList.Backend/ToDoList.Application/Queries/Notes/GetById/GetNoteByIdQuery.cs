using MediatR;
using ToDoList.Domain.Result;

namespace ToDoList.Application.Queries.Notes.GetById;

public sealed record GetNoteByIdQuery(int NoteId, int UserId) : IRequest<NoteResponse>;
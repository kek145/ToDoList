using MediatR;

namespace ToDoList.Application.Commands.Notes.Patch;

public sealed record CompleteNoteCommand(long NoteId, int UserId) : IRequest<long>;
using MediatR;

namespace ToDoList.Application.Commands.Notes.Patch;

public sealed record CompleteNoteCommand(int NoteId, int UserId) : IRequest;
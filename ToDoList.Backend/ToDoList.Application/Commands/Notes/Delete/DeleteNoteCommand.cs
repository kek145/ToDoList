using MediatR;

namespace ToDoList.Application.Commands.Notes.Delete;

public record DeleteNoteCommand(int UserId, int NoteId) : IRequest;
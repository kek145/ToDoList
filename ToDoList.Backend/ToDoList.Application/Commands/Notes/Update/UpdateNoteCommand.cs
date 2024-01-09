using MediatR;
using ToDoList.Domain.Request;

namespace ToDoList.Application.Commands.Notes.Update;

public sealed record UpdateNoteCommand(int UserId, int NoteId, NoteRequest NoteRequest) : IRequest;
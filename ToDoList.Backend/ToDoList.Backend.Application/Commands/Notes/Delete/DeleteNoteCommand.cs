using System.Net;
using MediatR;

namespace ToDoList.Application.Commands.Notes.Delete;

public record DeleteNoteCommand(int UserId, long NoteId) : IRequest<long>;
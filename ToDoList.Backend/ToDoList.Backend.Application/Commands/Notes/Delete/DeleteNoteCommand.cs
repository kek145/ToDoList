using System.Net;
using MediatR;

namespace ToDoList.Application.Commands.Notes.Delete;

public sealed record DeleteNoteCommand(int UserId, long NoteId) : IRequest<long>;
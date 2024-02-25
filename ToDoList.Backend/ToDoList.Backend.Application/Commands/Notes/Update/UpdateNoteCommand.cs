using System.Net;
using MediatR;
using ToDoList.Domain.Request;

namespace ToDoList.Application.Commands.Notes.Update;

public sealed record UpdateNoteCommand(int UserId, long NoteId, NoteRequest NoteRequest) : IRequest<long>;
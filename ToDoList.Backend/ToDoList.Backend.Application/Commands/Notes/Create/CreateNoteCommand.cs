﻿using MediatR;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Request;

namespace ToDoList.Application.Commands.Notes.Create;

public sealed record CreateNoteCommand(NoteRequest NoteRequest, int UserId) : IRequest<NoteDto>;
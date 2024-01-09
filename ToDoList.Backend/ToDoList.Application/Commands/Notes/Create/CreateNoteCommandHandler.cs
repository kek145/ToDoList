using System;
using MediatR;
using AutoMapper;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Application.Exceptions;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Result;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Commands.Notes.Create;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNoteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<NoteResponse> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = _mapper.Map<NoteDto>(request.NoteRequest);

        note.UserId = request.UserId;

        note.Priority = request.NoteRequest.Priority switch
        {
            Priority.Easy => PrioritiesHelper.Easy,
            Priority.Medium => PrioritiesHelper.Medium,
            Priority.Hard => PrioritiesHelper.Hard,
            _ => throw new BadRequestException("There is no such priority!")
        };

        var newNote = await _unitOfWork.Notes.AddNoteAsync(note, cancellationToken);

        if (newNote is null)
            throw new NullReferenceException("Note is null");

        await _unitOfWork.CommitAsync(cancellationToken);

        var result = _mapper.Map<NoteResponse>(newNote);

        return result;
    }
}
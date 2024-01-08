using System;
using MediatR;
using AutoMapper;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
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

        var newNote = await _unitOfWork.Notes.AddNoteAsync(note, cancellationToken);

        if (newNote is null)
            throw new NullReferenceException("Note is null");

        await _unitOfWork.CommitAsync(cancellationToken);

        var result = _mapper.Map<NoteResponse>(newNote);

        return result;
    }
}
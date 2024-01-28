using MediatR;
using AutoMapper;
using System.Threading;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Enum;
using System.Threading.Tasks;
using ToDoList.Domain.Helpers;
using ToDoList.Domain.Interfaces;
using ToDoList.Application.Exceptions;
using ToDoList.Domain.Interfaces;
using ToDoList.Domain.Repositories;

namespace ToDoList.Application.Commands.Notes.Create;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNoteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<NoteDto> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
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
        await _unitOfWork.CommitAsync(cancellationToken);

        return newNote;
    }
}
using MediatR;
using AutoMapper;
using System.Threading;
using ToDoList.Domain.Dto;
using ToDoList.Domain.Enum;
using System.Threading.Tasks;
using ToDoList.Domain.Helpers;
using ToDoList.Application.Exceptions;
using ToDoList.Domain.Repositories;

namespace ToDoList.Application.Commands.Notes.Create;

public class CreateNoteCommandHandler(IMapper mapper, INoteRepository noteRepository) : IRequestHandler<CreateNoteCommand, NoteDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly INoteRepository _noteRepository = noteRepository;

    public async Task<NoteDto> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = _mapper.Map<NoteDto>(request.NoteRequest);

        note.UserId = request.UserId;

        note.Priority = request.NoteRequest.Priority switch
        {
            Priority.Easy => PrioritiesHelper.Easy,
            Priority.Medium => PrioritiesHelper.Medium,
            Priority.Hard => PrioritiesHelper.Hard,
            _ => throw new BadRequestException("Такого пріоритету немає!")
        };

        var newNote = await _noteRepository.AddNoteAsync(note, cancellationToken);

        return newNote;
    }
}
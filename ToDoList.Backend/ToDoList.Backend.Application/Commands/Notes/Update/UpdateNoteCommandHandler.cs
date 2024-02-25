using MediatR;
using AutoMapper;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Repositories;

namespace ToDoList.Application.Commands.Notes.Update;

public class UpdateNoteCommandHandler(IMapper mapper, INoteRepository noteRepository) : IRequestHandler<UpdateNoteCommand, long>
{
    private readonly IMapper _mapper = mapper;
    private readonly INoteRepository _noteRepository = noteRepository;
    
    public async Task<long> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = _mapper.Map<NoteDto>(request.NoteRequest);
        
        var result = await _noteRepository.UpdateNoteAsync(request.NoteId, note, cancellationToken);

        return result;
    }
}
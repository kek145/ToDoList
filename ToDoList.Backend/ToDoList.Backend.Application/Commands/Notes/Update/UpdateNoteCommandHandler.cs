using MediatR;
using AutoMapper;
using System.Threading;
using ToDoList.Domain.Dto;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Commands.Notes.Update;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, long>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateNoteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<long> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = _mapper.Map<NoteDto>(request.NoteRequest);
        
        var result = await _unitOfWork.Notes.UpdateNoteAsync(request.NoteId, note, cancellationToken);

        return result;
    }
}
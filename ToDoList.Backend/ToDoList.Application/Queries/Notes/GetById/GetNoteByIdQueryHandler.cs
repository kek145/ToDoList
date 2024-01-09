using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Interfaces;
using ToDoList.Application.Exceptions;

namespace ToDoList.Application.Queries.Notes.GetById;

public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, NoteResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetNoteByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<NoteResponse> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var note = await _unitOfWork.Notes.GetNoteByIdAsync(request.NoteId, cancellationToken);

        if (note is null || note.UserId != request.UserId)
            throw new NotFoundException("Note not found!");

        var result = _mapper.Map<NoteResponse>(note);

        return result;
    }
}
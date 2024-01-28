using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Application.Exceptions;
using ToDoList.Domain.Interfaces;
using ToDoList.Domain.Repositories;

namespace ToDoList.Application.Commands.Notes.Delete;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteNoteCommand, long>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<long> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Notes.DeleteNoteAsync(request.NoteId, cancellationToken);
        return result;
    }
}
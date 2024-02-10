using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.Interfaces;

namespace ToDoList.Application.Commands.Notes.Delete;

public class DeleteTaskCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteNoteCommand, long>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<long> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Notes.DeleteNoteAsync(request.NoteId, cancellationToken);
        return result;
    }
}
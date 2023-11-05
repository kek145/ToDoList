using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ToDoList.DAL.Repositories.UnitOfWork;

namespace ToDoList.BL.Commands.TaskCommands;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTaskCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);

        task.Title = request.Request.Title;
        task.Description = request.Request.Description;
        task.Priority = request.Request.Priority;
        task.Deadline = request.Request.Deadline;
        task.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.TaskRepository.UpdateAsync(task);
        await _unitOfWork.CommitAsync();
    }
}
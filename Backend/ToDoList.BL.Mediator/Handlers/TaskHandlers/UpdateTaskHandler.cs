using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.BL.Mediator.Commands.TaskCommands;
using ToDoList.DAL.Contracts.Interfaces;

namespace ToDoList.BL.Mediator.Handlers.TaskHandlers;

public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTaskHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork
        )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork
            .TaskRepository
            .GetAll()
            .Where(find => find.Id == request.TaskId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (task == null)
            return false;

        task.UpdatedAt = DateTime.UtcNow;

        var result = _mapper.Map(request.Request, task);

        await _unitOfWork.TaskRepository.UpdateAsync(result);
        await _unitOfWork.CommitAsync();

        return true;
    }
}
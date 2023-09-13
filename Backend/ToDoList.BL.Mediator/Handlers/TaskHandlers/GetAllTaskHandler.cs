using System;
using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Contracts.Interfaces;
using ToDoList.Domain.Contracts.Response;
using ToDoList.BL.Mediator.Queries.TaskQueries;

namespace ToDoList.BL.Mediator.Handlers.TaskHandlers;

public class GetAllTaskHandler : IRequestHandler<GetAllTaskQuery, TaskResponse<GetTaskResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTaskHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<TaskResponse<GetTaskResponse>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
    {
        const float pageResult = 10f;
        var pageCount = Math.Ceiling(_unitOfWork.TaskRepository.GetAll().Count() / pageResult);
        
        var tasks = await _unitOfWork.TaskRepository
            .GetAll()
            .Where(x => x.Status == false)
            .Skip((request.Page - 1) * (int)pageResult)
            .Take((int)pageResult)
            .ToListAsync(cancellationToken: cancellationToken);
        
        var result = _mapper.Map<List<GetTaskResponse>>(tasks);

        return new TaskResponse<GetTaskResponse>
        {
            Items = result,
            CurrentPage = request.Page,
            Pages = (int)pageCount
        };
    }
}
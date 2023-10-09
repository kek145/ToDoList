﻿using System.Collections.Generic;

namespace ToDoList.BL.Mediator.Queries.TaskQueries;

public class GetAllTaskHandler : IRequestHandler<GetAllTaskQuery, PaginationResponse<GetTaskResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTaskHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<PaginationResponse<GetTaskResponse>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
    {
        const float pageResult = 10f;
        var pageCount = Math.Ceiling(_unitOfWork.TaskRepository.GetAll().Count() / pageResult);
        
        var tasks = await _unitOfWork.TaskRepository
            .GetAll()
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.UserId == request.UserId)
            .OrderBy(s => s.UpdatedAt)
            .Skip((request.Page - 1) * (int)pageResult)
            .Take((int)pageResult)
            .ToListAsync(cancellationToken: cancellationToken);

        var result = _mapper.Map<List<GetTaskResponse>>(tasks);

        return new PaginationResponse<GetTaskResponse>
        {
            Items = result,
            CurrentPage = request.Page,
            Pages = (int)pageCount
        };
    }
}
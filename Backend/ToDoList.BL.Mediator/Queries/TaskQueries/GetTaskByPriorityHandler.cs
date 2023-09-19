using System.Collections.Generic;
using ToDoList.Domain.Entities.Enums;

namespace ToDoList.BL.Mediator.Queries.TaskQueries;

public class GetTaskByPriorityHandler : IRequestHandler<GetAllTaskByPriorityQuery, PaginationResponse<GetTaskResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetTaskByPriorityHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<PaginationResponse<GetTaskResponse>> Handle(GetAllTaskByPriorityQuery request, CancellationToken cancellationToken)
    {
        return request.Priority switch
        {
            "Easy" => await GetTaskByPriorityAsync(Priority.Easy, request.Page, request.UserId),
            "Medium" => await GetTaskByPriorityAsync(Priority.Medium, request.Page, request.UserId),
            "Hard" => await GetTaskByPriorityAsync(Priority.Hard, request.Page, request.UserId),
            _ => null!
        };
    }

    private async Task<PaginationResponse<GetTaskResponse>> GetTaskByPriorityAsync(Priority priority, int page, int userId)
    {
        const float pageResult = 10f;
        var pageCount = Math.Ceiling(_unitOfWork.TaskRepository.GetAll().Count() / pageResult);

        var tasks = await _unitOfWork.TaskRepository
            .GetAll()
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.Priority == priority && x.UserId == userId)
            .OrderBy(s => s.UpdatedAt)
            .Skip((page - 1) * (int)pageResult)
            .Take((int)pageResult)
            .ToListAsync();

        var result = _mapper.Map<List<GetTaskResponse>>(tasks);
        
        return new PaginationResponse<GetTaskResponse>
        {
            Items = result,
            CurrentPage = page,
            Pages = (int)pageCount
        };
    }
}
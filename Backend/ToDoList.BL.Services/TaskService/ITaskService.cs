using System.Threading.Tasks;
using System.Collections.Generic;
using ToDoList.Domain.Contracts.Request;
using ToDoList.Domain.Contracts.Response;

namespace ToDoList.BL.Services.TaskService;

public interface ITaskService
{
    Task<bool> DeleteTaskAsync(int taskId);
    Task<GetTaskResponse> GetTaskByIdAsync(int taskId);
    Task<TaskResponse<GetTaskResponse>> GetAllTaskAsync(int page);
    Task<bool> UpdateTaskAsync(TaskRequest request, int taskId);
    Task<GetTaskResponse> CreateTaskAsync(TaskRequest request);
}
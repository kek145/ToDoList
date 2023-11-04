using System.Threading.Tasks;
using ToDoList.Domain.Contracts.Request;
using ToDoList.Domain.Contracts.Response;

namespace ToDoList.BL.Services.TaskService;

public interface ITaskService
{
    Task<GetTaskResponseDto> CreateTaskAsync(TaskRequestDto request);
    Task<GetTaskResponseDto> GetTaskByIdAsync(int taskId);
    Task<GetTaskResponseDto> GetAllTaskAsync();
    Task UpdateTaskAsync(TaskRequestDto request, int taskId);
    Task DeleteTaskAsync(int taskId);
}
namespace ToDoList.BL.Services.TaskService;

public interface ITaskService
{
    Task<GetTaskResponse> GetTaskByIdAsync(int taskId);
    Task UpdateTaskAsync(TaskRequest request, int taskId);
    Task DeleteTaskAsync(int taskId);
    Task<PaginationResponse<GetTaskResponse>> GetAllTasksAsync(int page, int userId);
    Task<GetTaskResponse> CreateTaskAsync(TaskRequest request, int userId);
}
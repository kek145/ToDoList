namespace ToDoList.BL.Services.TaskService;

public interface ITaskService
{
    Task DeleteTaskAsync(int taskId);
    Task CompleteTaskAsync(int taskId);
    Task<GetTaskResponse> GetTaskByIdAsync(int taskId);
    Task UpdateTaskAsync(TaskRequest request, int taskId);
    Task<GetTaskResponse> CreateTaskAsync(TaskRequest request);
    Task<PaginationResponse<GetTaskResponse>> GetAllTasksAsync(int page);
    Task<PaginationResponse<GetTaskResponse>> GetAllFailedTaskAsync(int page);
    Task<PaginationResponse<GetTaskResponse>> GetAllCompletedTaskAsync(int page);
    Task<PaginationResponse<GetTaskResponse>> SearchTaskAsync(int page, string search);
    Task<PaginationResponse<GetTaskResponse>> GetAllTasksByPriorityAsync(string priority, int page);
}
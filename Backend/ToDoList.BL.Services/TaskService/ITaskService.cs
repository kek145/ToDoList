namespace ToDoList.BL.Services.TaskService;

public interface ITaskService
{
    Task DeleteTaskAsync(int taskId);
    Task CompleteTaskAsync(int taskId);
    Task<GetTaskResponse> GetTaskByIdAsync(int taskId);
    Task UpdateTaskAsync(TaskRequest request, int taskId);
    Task<GetTaskResponse> CreateTaskAsync(TaskRequest request, int userId);
    Task<PaginationResponse<GetTaskResponse>> GetAllTasksAsync(int page, int userId);
    Task<PaginationResponse<GetTaskResponse>> GetAllFailedTaskAsync(int page, int userId);
    Task<PaginationResponse<GetTaskResponse>> GetAllCompletedTaskAsync(int page, int userId);
    Task<PaginationResponse<GetTaskResponse>> SearchTaskAsync(int page, int userId, string search);
    Task<PaginationResponse<GetTaskResponse>> GetAllTasksByPriorityAsync(string priority, int page, int userId);
}
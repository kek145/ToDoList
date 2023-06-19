using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using System.Collections.Generic;

namespace ToDoList.Services.Interfaces
{
    public interface ITaskService
    {
        Task UpdateTaskAsync(TaskEntity task, int taskId);
        Task DeleteTaskAsync(int taskId, int userId);
        Task CreateTaskAsync(TaskEntity task);
        Task<IEnumerable<TaskEntity>> GetTasksByUserIdAsync(int userId);
    }
}
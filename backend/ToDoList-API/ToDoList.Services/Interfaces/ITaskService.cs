using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using System.Collections.Generic;

namespace ToDoList.Services.Interfaces
{
    public interface ITaskService
    {
        Task UpdateTaskAsync(TaskEntity task);
        Task DeleteTaskAsync(int taskId, int userId);
        Task CreateTaskAsync(TaskEntity task, int userId);
        Task<IEnumerable<TaskEntity>> GetTasksByUserIdAsync(int userId);
    }
}
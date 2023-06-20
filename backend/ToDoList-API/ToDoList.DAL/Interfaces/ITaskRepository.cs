using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using System.Collections.Generic;

namespace ToDoList.DAL.Interfaces
{
    public interface ITaskRepository
    {
        Task CreateTaskAsync(TaskEntity entity);
        Task UpdateTaskAsync(TaskEntity entity);
        Task DeleteTaskAsync(TaskEntity entity);
        Task<TaskEntity> GetTaskByIdAsync(int taskId);
        Task<IEnumerable<TaskEntity>> GetTasksByUserIdAsync(int userId);
    }
}
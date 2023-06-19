using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using System.Collections.Generic;

namespace ToDoList.DAL.Interfaces
{
    public interface ITaskRepository
    {
        Task CreateTaskAsync(TaskEntity entity);
        Task UpdateTaskAsync(TaskEntity entity, int taskId);
        Task DeleteTaskAsync(int taskId, int userId);
        Task<IEnumerable<TaskEntity>> GetTasksByUserIdAsync(int userId);
    }
}
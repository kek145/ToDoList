using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using System.Collections.Generic;

namespace ToDoList.DAL.Interfaces
{
    public interface ITaskRepository
    {
        Task CreateTaskAsync(TaskEntity entity, int userId);
        Task UpdateTaskAsync(TaskEntity entity);
        Task DeleteTaskAsync(int taskId, int userId);
        Task<IEnumerable<TaskEntity>> GetTasksByUserIdAsync(int userId);
    }
}
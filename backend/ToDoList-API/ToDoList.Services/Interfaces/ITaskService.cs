using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using System.Collections.Generic;
using ToDoList.Services.Models.Dto;

namespace ToDoList.Services.Interfaces
{
    public interface ITaskService
    {
        Task DeleteTaskAsync(int taskId, string token);
        Task UpdateTaskAsync(TaskDto taskDto, string token, int taskId);
        Task CreateTaskAsync(TaskDto taskDto, string token);
        Task<IEnumerable<TaskEntity>> GetTasksByUserIdAsync(string token);
    }
}
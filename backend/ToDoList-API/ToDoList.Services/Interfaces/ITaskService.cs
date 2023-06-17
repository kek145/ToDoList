using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using ToDoList.Domain.Enum;

namespace ToDoList.Services.Interfaces
{
    public interface ITaskService
    {
        Task DeleteTaskItemAsync(int taskid);
        Task<List<TaskEntity>> GetAllTaskItemsAsync(int userId);
        Task<TaskEntity> UpdateTaskItemAsync(int taskid, string title, string description, bool status, Priority priority, DateTime created);
        Task<TaskEntity> CreateTaskItemAsync(int userid, string title, string description, bool status, Priority priority, DateTime created);
    }
}

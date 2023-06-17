using System;
using System.Collections.Generic;
using ToDoList.Domain.Enum;
using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using ToDoList.DAL.Interfaces;
using ToDoList.Services.Interfaces;

namespace ToDoList.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskEntity> CreateTaskItemAsync(int userid, string title, string description, bool status, Priority priority, DateTime created)
        {
            var user = await _taskRepository.GetByIdAsync(userid);

            if (user is null)
                return null!;

            var task = new TaskEntity
            {
                Title = title,
                Description = description,
                Status = status,
                Priority = priority,
                CreatedDate = created,
                UserID = userid
            };
            return await _taskRepository.CreateAsync(task);
        }

        public async Task DeleteTaskItemAsync(int taskid)
        {
            var task = await _taskRepository.GetByIdAsync(taskid);

            if (task is null)
                throw new Exception("ToDoItem not found");

            await _taskRepository.DeleteAsync(taskid);
        }

        public async Task<List<TaskEntity>> GetAllTaskItemsAsync(int userid)
        {
            return await _taskRepository.GetAllAsync(userid);
        }

        public async Task<TaskEntity> UpdateTaskItemAsync(int taskid, string title, string description, bool status, Priority priority, DateTime created)
        {
            var task = await _taskRepository.GetByIdAsync(taskid);

            if (task is null)
                return null!;

            task.Title = title;
            task.Description = description;
            task.Status = status;
            task.Priority = priority;
            task.CreatedDate = created;

            return await _taskRepository.UpdateAsync(task);
        }
    }
}

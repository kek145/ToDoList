using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.DAL.Interfaces;
using ToDoList.Domain.Entity;
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
        public async Task UpdateTaskAsync(TaskEntity task, int taskId)
        {
            await _taskRepository.UpdateTaskAsync(task, taskId);
        }

        public async Task DeleteTaskAsync(int taskId, int userId)
        {
            await _taskRepository.DeleteTaskAsync(taskId, userId);
        }

        public async Task CreateTaskAsync(TaskEntity task)
        {
            await _taskRepository.CreateTaskAsync(task);
        }

        public async Task<IEnumerable<TaskEntity>> GetTasksByUserIdAsync(int userId)
        {
            return await _taskRepository.GetTasksByUserIdAsync(userId);
        }
    }
}
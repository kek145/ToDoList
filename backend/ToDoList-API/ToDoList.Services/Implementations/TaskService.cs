using System;
using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using ToDoList.DAL.Interfaces;
using System.Collections.Generic;
using ToDoList.Services.Interfaces;
using ToDoList.Services.Models.Dto;

namespace ToDoList.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public TaskService(ITaskRepository taskRepository, IJwtTokenService jwtTokenService)
        {
            _taskRepository = taskRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task DeleteTaskAsync(int taskId, string token)
        {
            int? userId = _jwtTokenService.GetUserIdFromToken(token);
            if (userId.HasValue)
            {
                var taskEntity = await _taskRepository.GetTaskByIdAsync(taskId);
                if (taskEntity == null)
                    throw new Exception("Task not found");

                if (taskEntity.UserID != userId.Value)
                    throw new UnauthorizedAccessException("User is not authorized to delete this task");

                await _taskRepository.DeleteTaskAsync(taskEntity);
            }
        }

        public async Task UpdateTaskAsync(TaskDto taskDto, string token, int taskId)
        {
            int? userId = _jwtTokenService.GetUserIdFromToken(token);
            if (userId.HasValue)
            {
                var taskEntity = await _taskRepository.GetTaskByIdAsync(taskId);
                if (taskEntity == null)
                    throw new Exception("Task not found");

                if (taskEntity.UserID != userId.Value)
                    throw new UnauthorizedAccessException("User is not authorized to update this task");

                taskEntity.Title = taskDto.Title;
                taskEntity.Description = taskDto.Description;
                taskEntity.Status = taskDto.Status;
                taskEntity.Priority = taskDto.Priority;
                taskEntity.CreatedDate = taskDto.CreatedDate;

                await _taskRepository.UpdateTaskAsync(taskEntity);
            }
        }

        public async Task CreateTaskAsync(TaskDto taskDto, string token)
        {
            int? userId = _jwtTokenService.GetUserIdFromToken(token);
            if (userId.HasValue)
            {
                var task = new TaskEntity
                {
                    Title = taskDto.Title,
                    Description = taskDto.Description,
                    Status = taskDto.Status,
                    Priority = taskDto.Priority,
                    CreatedDate = taskDto.CreatedDate,
                    UserID = userId.Value
                };
                await _taskRepository.CreateTaskAsync(task);
            }
        }


        public async Task<IEnumerable<TaskEntity>> GetTasksByUserIdAsync(string token)
        {
            int? userId = _jwtTokenService.GetUserIdFromToken(token);
            if (userId is null)
                return null!;
            return await _taskRepository.GetTasksByUserIdAsync(userId!.Value);
        }
    }
}
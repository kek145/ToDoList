using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Core;
using ToDoList.DAL.Interfaces;
using ToDoList.Domain.Entity;

namespace ToDoList.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationContext _db;

        public TaskRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task CreateTaskAsync(TaskEntity entity)
        {
            await _db.Set<TaskEntity>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskEntity entity, int taskId)
        {
            var existingTask = await _db.TasksEntity.FindAsync(taskId);

            if (existingTask is not null)
            {
                existingTask.Title = entity.Title.Trim();
                existingTask.Description = entity.Description.Trim();
                existingTask.Status = entity.Status;
                existingTask.Priority = entity.Priority;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteTaskAsync(int taskId, int userId)
        {
            var task = await _db.TasksEntity.FirstOrDefaultAsync(task => task.TaskId == taskId && task.UserID == userId);

            if (task is not null)
            {
                _db.TasksEntity.Remove(task);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TaskEntity>> GetTasksByUserIdAsync(int userId)
        {
            return await _db.TasksEntity
                .Where(task => task.UserID == userId)
                .ToListAsync();
        }
    }
}
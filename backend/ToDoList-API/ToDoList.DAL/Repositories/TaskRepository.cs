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
            _db.TasksEntity.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskEntity entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(TaskEntity entity)
        {
            _db.TasksEntity.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<TaskEntity> GetTaskByIdAsync(int taskId)
        {
            var task = await _db.TasksEntity.FindAsync(taskId);
            if (task is null)
                return null!;

            return task;
        }

        public async Task<IEnumerable<TaskEntity>> GetTasksByUserIdAsync(int userId)
        {
            return await _db.TasksEntity
                .Where(task => task.UserID == userId)
                .ToListAsync();
        }
    }
}
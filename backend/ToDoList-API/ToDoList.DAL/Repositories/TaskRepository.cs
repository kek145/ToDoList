using System.Linq;
using ToDoList.DAL.Core;
using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using ToDoList.DAL.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationContext _db;
        public TaskRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<TaskEntity> CreateAsync(TaskEntity entity)
        {
            await _db.TasksEntity.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int taskid)
        {
            var task = await _db.TasksEntity.FindAsync(taskid);
            if (task is not null) 
            {
                _db.TasksEntity.Remove(task);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<TaskEntity>> GetAllAsync(int userid)
        {
            var result = await _db.TasksEntity.Where(t => t.UserID == userid).ToListAsync();
            return result;
        }

        public async Task<TaskEntity> GetByIdAsync(int taskid)
        {
            var result = await _db.TasksEntity.FindAsync(taskid);
            return result!;
        }

        public async Task<TaskEntity> UpdateAsync(TaskEntity entity)
        {
            _db.TasksEntity.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

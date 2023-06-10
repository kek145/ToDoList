using System.Linq;
using ToDoList.DAL.Core;
using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using ToDoList.DAL.Interfaces;

namespace ToDoList.DAL.Repositories
{
    public class TaskRepository : IBaseRepository<TaskEntity>
    {
        private readonly ApplicationContext _db;
        public TaskRepository(ApplicationContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(TaskEntity entity)
        {
            await _db.Tasks.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskEntity entity)
        {
            _db.Tasks.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<TaskEntity> GetAllAsync()
        {
            return _db.Tasks;
        }

        public async Task<TaskEntity> UpdateAsync(TaskEntity entity)
        {
            _db.Tasks.Update(entity);
            await _db.SaveChangesAsync();
            
            return entity;
        }
    }
}
